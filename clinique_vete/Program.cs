using System;
using MySql.Data.MySqlClient;
using clinique_vete.cliniquevt;
using System.Collections;
using System.Text.RegularExpressions;

sqldata mysqldata = new sqldata();
List<Animals> listanimal = new List<Animals>();

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
    Console.WriteLine("Ajouter un animal: ");

    while (validationMaxAnimal()) { } 
    
    // insert type animal
    do
    {
        Console.WriteLine("Veuillez saisir le type de l'animal: ");
        animal1.typeanimal = Console.ReadLine();

    } while (validationstring(animal1.typeanimal) || animal1.typeanimal=="");

    //insert nom animal
    do
    {
        Console.WriteLine("Veuillez saisir le nom de l'animal: ");
        animal1.nomanimal = Console.ReadLine();

    } while (validationstring(animal1.nomanimal) || animal1.nomanimal == "");

    // insert age animal
    bool isInt;
    int age;
    do
    {
        Console.WriteLine("Veuillez saisir l'age de l'animal: ");
        isInt = int.TryParse(Console.ReadLine(), out age);
         
    } while (validationint(age) || !isInt);
     animal1.ageanimal = age;

    // insert poids animal
    bool isdecimal;
    decimal poids;
    do
    {
        Console.WriteLine("Veuillez saisir le poids de l'animal: ");
        isdecimal = decimal.TryParse(Console.ReadLine(), out poids);
        if (isdecimal == false)
        {
            Console.WriteLine("Le choix n'est pas valide...");
        }
    } while (!isdecimal || poids < 0);
    animal1.poidanimal = poids;

    //insert couleur animal
    do
    {
        Console.WriteLine("Veuillez saisir la couleur de l'animal (rouge, violet,bleu): ");
        animal1.couleuranimal = Console.ReadLine();

    } while ((animal1.couleuranimal != "rouge") && (animal1.couleuranimal != "bleu") && (animal1.couleuranimal != "violet") || validationstring(animal1.couleuranimal));

    // insert proprietaire
    do
    {
        Console.WriteLine("Veuillez saisir le proprietaire de l'animal: ");
        animal1.propanimal = Console.ReadLine(); 

    } while (validationstring(animal1.propanimal) || animal1.propanimal == "");
    
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
    conn.Close();
    
    Console.Clear();
}


void tableau()  // Voir la liste de tous les animaux en pension (option 2)
{
    Console.WriteLine("liste de tous les animaux en pension: ");
    Console.WriteLine("--------------------------------------------------------------------------");
    Console.WriteLine("| ID " +"\t"+ "| TYPE ANIMAL" + "\t"  +"| NOM" +"\t"+ "| AGE" + "\t"  + "| POIDS" + "\t"+ "| COULEUR" + "  " + "| PROPRIETAIRE |");
    Console.WriteLine("--------------------------------------------------------------------------");

    MySqlConnection conn = mysqldata.connectTobase();
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
    Console.WriteLine("--------------------------------------------------------------------------");
    conn.Close();   

    Console.ReadKey();
    Console.Clear();
}

void printanimals(Animals animal1)
{
   Console.WriteLine("|  "+ animal1.ID + "\t"+
                           "| " + animal1.typeanimal + "\t" + "\t" +
                           "| " + animal1.nomanimal +  "\t"+ 
                           "| " + animal1.ageanimal +  "\t" +
                           "| " + animal1.poidanimal + "\t" +
                           "| " + animal1.couleuranimal +  "    " +
                           "| " + animal1.propanimal + "\t" + "  |");
}


void proprietaire()  //Voir la liste de tous les propriétaires (option 3)
{
    Console.WriteLine("Dans la fonction voir la liste animaux pension  ");
    Console.WriteLine("-----------------");
    Console.WriteLine("| PROPRIETAIRE |");
    Console.WriteLine("-----------------");

    MySqlConnection conn = mysqldata.connectTobase();
    MySqlCommand command = new MySqlCommand("select proprietaire from animal", conn);
    using (MySqlDataReader reader = command.ExecuteReader())
    {
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Animals animal1 = new Animals();
                animal1.propanimal = reader.GetString("proprietaire");
                Console.WriteLine("   " + animal1.propanimal + "\t" + "  |");
            }
        }
    }
    Console.WriteLine("-----------------");
    conn.Close();

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
    MySqlCommand command = new MySqlCommand("select count(*) nbranimal from animal", conn);
    using (MySqlDataReader reader = command.ExecuteReader())
    {
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string resutnbranimal = reader.GetString("nbranimal");
                Console.WriteLine("|   " + resutnbranimal + "\t" + "\t" + " |");
            }
        }
    }
    Console.WriteLine("-----------------");
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

    MySqlConnection conn = mysqldata.connectTobase();
    MySqlCommand command = new MySqlCommand("select sum(poids) sumpoids from animal", conn);
    using (MySqlDataReader reader = command.ExecuteReader())
    {
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string resutsumpoids = reader.GetString("sumpoids");
                Console.WriteLine("|  " + resutsumpoids + "\t" + "|");
            }
        }
    }
    Console.WriteLine("-----------------");
    conn.Close();

    Console.ReadKey();
    Console.Clear();
}

void coloranimal() //Voir la liste des animaux selon la couleur (option 6)
{
    Animals animal1 = new Animals();
    Console.WriteLine("Liste des animaux d’une couleur: ");
    
    do
    {
        Console.WriteLine("Veuillez saisir la couleur de recherche:   ");
        animal1.couleuranimal = Console.ReadLine();

    } while (animal1.couleuranimal != "rouge" && animal1.couleuranimal != "violet" && animal1.couleuranimal != "bleu" || validationstring(animal1.couleuranimal));

    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("| ID " + "\t" + "| TYPE ANIMAL" + "\t" + "| NOM" + "\t" + "| COULEUR "+"|");
    Console.WriteLine("-------------------------------------------");

    MySqlConnection conn = mysqldata.connectTobase();
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

                Console.WriteLine("|  " + animal1.ID + "\t" +
                                  "| " + animal1.typeanimal + "\t" + "\t" +
                                  "| " + animal1.nomanimal + "\t" +
                                  "| " + animal1.couleuranimal + "\t" + "  |");
                
            }
        }
    }
    Console.WriteLine("-------------------------------------------");
    conn.Close();

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
    } while (!isValide || validationIdExist(IDtoremove));
   
    MySqlConnection conn = mysqldata.connectTobase();
    MySqlCommand command = new MySqlCommand("delete from animal where id = @id", conn);
    command.Parameters.AddWithValue("@id", IDtoremove);
    command.ExecuteReader();
    conn.Close();
    tableau();
   
    Console.ReadKey();
    Console.Clear();
}

void modifierlist() // modifier la liste (option 8)
{
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

    } while (!isValide || validationIdExist(IDamodifier));

    do //insert new nom
    {
        Console.Write("Nouveau nom de l’animal : ");
        animal1.nomanimal = Console.ReadLine();

    } while (validationstring(animal1.nomanimal) || animal1.nomanimal == "");

    bool isInt;
    int age;
    do // insert new age
    {
        Console.Write("Nouveau age de l'animal: ");
        isInt = int.TryParse(Console.ReadLine(), out age);

    } while (validationint(age) || !isInt);
    animal1.ageanimal = age;

    // insert poids animal
    bool isdecimal;
    decimal poids;
    do
    {
        Console.Write("Nouveau poids de l'animal: ");
        isdecimal = decimal.TryParse(Console.ReadLine(), out poids);
        if (isdecimal == false)
        {
            Console.WriteLine("Le choix n'est pas valide...");
        }
    } while (!isdecimal || poids < 0);
    animal1.poidanimal = poids;

    //insert couleur animal
    do
    {
        Console.Write("Nouvelle couleur de l'animal (rouge, violet,bleu): ");
        animal1.couleuranimal = Console.ReadLine();

    } while ((animal1.couleuranimal != "rouge") && (animal1.couleuranimal != "bleu") && (animal1.couleuranimal != "violet") || validationstring(animal1.couleuranimal));

    // insert proprietaire
    do
    {
        Console.Write("Nouveau proprietaire de l'animal: ");
        animal1.propanimal = Console.ReadLine();

    } while (validationstring(animal1.propanimal) || animal1.propanimal == "");

    MySqlConnection conn = mysqldata.connectTobase();
    MySqlCommand command = new MySqlCommand("update animal set nom = @nom, age = @age, poids = @poids, couleur = @couleur, proprietaire =@proprietaire  where id = @id", conn);
  
    command.Parameters.AddWithValue("@id", IDamodifier);
    command.Parameters.AddWithValue("@nom", animal1.nomanimal);
    command.Parameters.AddWithValue("@age", animal1.ageanimal);
    command.Parameters.AddWithValue("@poids", animal1.poidanimal);
    command.Parameters.AddWithValue("@couleur", animal1.couleuranimal);
    command.Parameters.AddWithValue("@proprietaire", animal1.propanimal);

    command.ExecuteReader();
    conn.Close();

    Console.WriteLine();
    Console.WriteLine ("Le nom du pensionnaire" + " '" + IDamodifier + "' " + " a été modifié.");
    Console.ReadLine();
    Console.Clear();
}

bool validationIdExist(int IDamodifier)  //condition si le id exist
{
    int exist = 0;
    bool valide = false;
    MySqlConnection conn1 = mysqldata.connectTobase();
    MySqlCommand command2 = new MySqlCommand("select count(*) exist from animal where id=@id2", conn1);
    command2.Parameters.AddWithValue("@id2", IDamodifier);
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
    if (exist==0)
    {
        valide = true;
        Console.WriteLine("Votre choix n'existe pas");
    }
    command2.ExecuteReader();
    conn1.Close();
    return valide;
}

 bool validationstring(string str)
{
    bool valide = false;
    if (!Regex.Match(str, "^[a-zA-Z]*$").Success) 
    {
        valide = true;
        Console.WriteLine("Le choix n'est pas valide...");
    }
    return valide;
}

bool validationint(int inputint)
{
    bool valide=false;
    if (!(inputint > 0 && inputint < 100))
    {
        valide = true;
        Console.WriteLine("Le choix n'est pas valide...");
    }
    return valide;
}

bool validationMaxAnimal() //condition limite de places
{
    int exist = 0;
    bool valide = false;
    MySqlConnection conn1 = mysqldata.connectTobase();
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
    conn1.Close();
    return valide;
}












