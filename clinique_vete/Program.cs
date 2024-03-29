﻿using System;
using MySql.Data.MySqlClient;
using clinique_vete.cliniquevt;
using System.Collections;
using System.Text.RegularExpressions;

sqldata mysqldata = new sqldata();
List<Animals> listanimal = new List<Animals>();
validation validation = new validation();

const string rouge = "rouge";
const string violet = "violet";
const string bleu = "bleu";

selectchoice();

void affichermenu() //menu principal
{
    Console.WriteLine("1- Ajouter un animal");
    Console.WriteLine("2- Liste de tous les animaux en pension");
    Console.WriteLine("3- Liste des proprietaire");
    Console.WriteLine("4- Le nombre total d’animaux en pension ");
    Console.WriteLine("5- Le poids total de tous les animaux en pension");
    Console.WriteLine("6- Liste des animaux d’une couleur ");
    Console.WriteLine("7- Retirer un animal de la liste");
    Console.WriteLine("8- Modifier un animal de la liste");
    Console.WriteLine("9- Quitter");
    Console.WriteLine("Choisir une option");
}

void makechoice()
{
    string option2 = Console.ReadLine();

    switch (option2)
    {
        case "1":
            addanimal();
            break;

        case "2":
            tableau();
            break;

        case "3":
            proprietaire();
            break;

        case "4":
            sommetab();
            break;

        case "5":
            poidstotal();
            break;

        case "6":
            coloranimal();
            break;

        case "7":
            deleteanimal();
            break;

        case "8":
            modifierlist();
            break;

        case "9":
            Quitter();
            break;

        default:
            {
                Console.WriteLine("Le choix n'est pas valide...");
                break;
            }
    }
}
void selectchoice()
{
    int option = 0;
    do
    {
        affichermenu();
        makechoice();
    } while (option > 8 || option < 1);
}

void Quitter() // (option 9)
{
    Environment.Exit(0);
}

 void addanimal() // Ajouter un animal (option 1)
{
    Animals animal1 = new Animals();
    MySqlConnection conn = mysqldata.connectTobase();
    insert insert = new insert();

    Console.WriteLine("Ajouter un animal: ");
    while (validationMaxAnimal()) { }
    
    insert.insererinfo(animal1);

    try
    {
        string sql = "INSERT INTO animal(typeanimal,nom,age,poids,couleur,proprietaire)" +
                     "VALUES (@typeanimal,@nomanimal,@ageanimal,@poidanimal,@couleuranimal,@propanimal)";

        MySqlCommand command = new MySqlCommand(sql, conn);
        command.Parameters.AddWithValue("@typeanimal", animal1.typeanimal);
        command.Parameters.AddWithValue("@nomanimal", animal1.nomanimal);
        command.Parameters.AddWithValue("@ageanimal", animal1.ageanimal);
        command.Parameters.AddWithValue("@poidanimal", animal1.poidanimal);
        command.Parameters.AddWithValue("@couleuranimal", animal1.couleuranimal);
        command.Parameters.AddWithValue("@propanimal", animal1.propanimal);
        command.ExecuteReader();
        Console.WriteLine("Insertion est terminé avec succès");
    }
    catch (Exception ex)
    {
        Console.WriteLine("insertion non invalid " + ex.Message);
    }
    finally
    {
        conn.Close();
    }
    Console.ReadKey();
    Console.Clear();
}

void tableau()  // Voir la liste de tous les animaux en pension (option 2)
{
    Console.WriteLine("liste de tous les animaux en pension: ");
    Console.WriteLine("------------------------------------------------------------------------------");
    Console.WriteLine("| ID " +"\t"+ "| TYPE ANIMAL" + "\t"  +"| NOM" +"\t"+ "| AGE" + "\t"  + "| POIDS" + "\t"+"   "+ "| COULEUR" + "  " + "| PROPRIETAIRE |");
    Console.WriteLine("------------------------------------------------------------------------------");

    MySqlConnection conn = mysqldata.connectTobase();
    try
    {
        MySqlCommand command = new MySqlCommand("select * from animal order by id", conn);

        using (MySqlDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Animals animal1 = new Animals();
                    animal1.ID = reader.GetInt32("id");
                    animal1.typeanimal = reader.GetString("typeanimal");
                    animal1.nomanimal = reader.GetString("nom");
                    animal1.ageanimal = reader.GetInt32("age");
                    animal1.poidanimal = reader.GetDecimal("poids");
                    animal1.couleuranimal = reader.GetString("couleur");
                    animal1.propanimal = reader.GetString("proprietaire");

                    listanimal.Add(animal1);
                    printanimals(animal1);
                }
            }
        }
        Console.WriteLine("------------------------------------------------------------------------------");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Requete non valide" + ex.Message);
    }
    finally
    {
        conn.Close();
    }
    Console.ReadKey();
    Console.Clear();
}

void printanimals(Animals animal1)
{
   Console.Write(animal1.ID.ToString().PadLeft(3));
   Console.Write(animal1.typeanimal.PadLeft(15));
   Console.Write(animal1.nomanimal.PadLeft(12));
   Console.Write(animal1.ageanimal.ToString().PadLeft(8));
   Console.Write(animal1.poidanimal.ToString().PadLeft(10));
   Console.Write(animal1.couleuranimal.PadLeft(12));
   Console.WriteLine(animal1.propanimal.PadLeft(10));
}

void proprietaire()  //Voir la liste de tous les propriétaires (option 3)
{
    Console.WriteLine("Dans la fonction voir la liste animaux pension  ");
    Console.WriteLine("-----------------");
    Console.WriteLine("| PROPRIETAIRE |");
    Console.WriteLine("-----------------");

    MySqlConnection conn = mysqldata.connectTobase();
    try
    {
        MySqlCommand command = new MySqlCommand("select proprietaire from animal", conn);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Animals animal1 = new Animals();
                    animal1.propanimal = reader.GetString("proprietaire");
                    Console.WriteLine("   " + animal1.propanimal );
                }
            }
        }
        Console.WriteLine("-----------------");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Requete non valide" + ex.Message);
    }
    finally
    {
        conn.Close();
    }
    Console.ReadKey();
    Console.Clear();
}

void sommetab() // Voir le nombre total d’animaux en pension (option 4)
{
    Console.WriteLine("le nombre total d’animaux en pension: ");

    Console.WriteLine("-------------------");
    Console.WriteLine("| NOMBRE ANIMAUX |");
    Console.WriteLine("-------------------");

    MySqlConnection conn = mysqldata.connectTobase();
    try
    {
        MySqlCommand command = new MySqlCommand("select count(*) nbranimal from animal", conn);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string resutnbranimal = reader.GetString("nbranimal");
                    Console.WriteLine("   " + resutnbranimal );
                }
            }
        }
        Console.WriteLine("-----------------");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Requete non valide" + ex.Message);
    }
    finally
    {
        conn.Close();
    }

    conn.Close();

    Console.ReadKey();
    Console.Clear();
}

void poidstotal() // Voir le poids total de tous les animaux en pension (option 5)
{
    Console.WriteLine("Le poids total de tous les animaux en pension: ");

    Console.WriteLine("-----------------");
    Console.WriteLine("| POIDS TOTALE  |");
    Console.WriteLine("------------------");

    MySqlConnection conn = mysqldata.connectTobase();  /*open connexion*/
    try
    {
        MySqlCommand command = new MySqlCommand("select sum(poids) sumpoids from animal", conn);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string resutsumpoids = reader.GetString("sumpoids");
                    Console.WriteLine("   " + resutsumpoids );
                }
            }
        }
        Console.WriteLine("-----------------");
        
    }
    catch (Exception ex)
    {
        Console.WriteLine("impossible d'ouvrir la connexion" + ex.Message);
    }
    finally
    {
        conn.Close();
    }

    Console.ReadKey();
    Console.Clear();
}

void coloranimal() //Voir la liste des animaux selon la couleur (option 6)
{
    Animals animal1 = new Animals();
    insert insert2 = new insert();
    Console.WriteLine("Liste des animaux d’une couleur: ");
    
    do
    {
        Console.WriteLine("Veuillez saisir la couleur de recherche:   ");
        animal1.couleuranimal = Console.ReadLine().ToLower();

    } while (animal1.couleuranimal != rouge && animal1.couleuranimal != violet && animal1.couleuranimal != bleu || validation.validationString(animal1.couleuranimal));

    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("| ID " + "\t" + "| TYPE ANIMAL" + "\t" + "| NOM" + "\t" + "| COULEUR "+"|");
    Console.WriteLine("-------------------------------------------");

    MySqlConnection conn = mysqldata.connectTobase();
    try
    {
        MySqlCommand command = new MySqlCommand("select id, typeanimal, nom, couleur from animal where couleur = @thecolor", conn);
        command.Parameters.AddWithValue("@thecolor", animal1.couleuranimal);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    animal1.ID = reader.GetInt32("id");
                    animal1.typeanimal = reader.GetString("typeanimal");
                    animal1.nomanimal = reader.GetString("nom");
                    animal1.couleuranimal = reader.GetString("couleur");

                    Console.WriteLine("   " + animal1.ID + "\t" +
                                      "  " + animal1.typeanimal + "\t" + "\t" +
                                      "  " + animal1.nomanimal + "\t" +
                                      "  " + animal1.couleuranimal + "\t");

                }
            }
        }
        Console.WriteLine("-------------------------------------------");
    }
    catch (Exception ex)
    {
        Console.WriteLine("impossible d'ouvrir la connexion" + ex.Message);
    }
    finally
    {
        conn.Close();
    }
    Console.ReadKey();
    Console.Clear();
}

void deleteanimal() //Retirer un animal de la liste (option 7)
{
    Animals animal1 = new Animals();
    int IDtoremove;
    bool isValide;
    do
    {
        Console.WriteLine("Inserer le ID de l'animal que vous voulez retirer de la liste: ");
        isValide = int.TryParse(Console.ReadLine(), out IDtoremove);
        if (isValide == false)
        {
            Console.WriteLine("Le choix n'est pas valide...");
        }
    } while (!isValide || validation.validationIdExist(IDtoremove));
   
    MySqlConnection conn = mysqldata.connectTobase();
    try
    {
        MySqlCommand command = new MySqlCommand("delete from animal where id = @id", conn);
        command.Parameters.AddWithValue("@id", IDtoremove);
        command.ExecuteReader();
    }
    catch (Exception ex)
    {
        Console.WriteLine("impossible d'ouvrir la connexion" + ex.Message);
    }
    finally
    {
        conn.Close();
    }

    tableau();
    Console.ReadKey();
}


void modifierlist() // modifier la liste (option 8)
{
    insert insert1 = new insert();
    Animals animal1 = new Animals();

    int IDamodifier;
    bool isValide;
    do // choisir ID to update
    {
        Console.Write("Numéro de l’animal à modifier : ");
        isValide = int.TryParse(Console.ReadLine(), out IDamodifier);
        if (isValide == false)
        {
            Console.WriteLine("Le choix n'est pas valide...");
        }

    } while (!isValide || validation.validationIdExist(IDamodifier));

    insert1.InsertInfoModification(animal1);

    MySqlConnection conn = mysqldata.connectTobase();
    try
    {
        MySqlCommand command = new MySqlCommand("update animal set nom = @nom, age = @age, poids = @poids, couleur = @couleur, proprietaire =@proprietaire  where id = @id", conn);

        command.Parameters.AddWithValue("@id", IDamodifier);
        command.Parameters.AddWithValue("@nom", animal1.nomanimal);
        command.Parameters.AddWithValue("@age", animal1.ageanimal);
        command.Parameters.AddWithValue("@poids", animal1.poidanimal);
        command.Parameters.AddWithValue("@couleur", animal1.couleuranimal);
        command.Parameters.AddWithValue("@proprietaire", animal1.propanimal);
        command.ExecuteReader();
        Console.WriteLine("Le nom du pensionnaire" + " '" + IDamodifier + "' " + " a été modifié.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Update impossible" + ex.Message);
    }
    finally
    {
        conn.Close();
    }
    Console.ReadLine();
    Console.Clear();
}

bool validationMaxAnimal() //condition limite de places
{
    int exist = 0;
    bool valide = false;
    MySqlConnection conn1 = mysqldata.connectTobase();
    try
    {
        MySqlCommand command2 = new MySqlCommand("select count(*) exist from animal", conn1);
        command2.Parameters.AddWithValue("exist", exist);
        using (MySqlDataReader reader = command2.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    exist = reader.GetInt32("exist");
                }
            }
        }
        if (exist >= 30)
        {
            valide = true;
            Console.WriteLine();
            Console.WriteLine("**Vous avez atteint la limite maximal des pensionnaires**");
            Console.WriteLine();
            selectchoice();
        }
        else
        {
            valide = false;
        }
        command2.ExecuteReader();
    }
    catch (Exception ex)
    {
        Console.WriteLine("impossible " + ex.Message);
    }
    finally
    {
        conn1.Close();
    }
    return valide;
}













