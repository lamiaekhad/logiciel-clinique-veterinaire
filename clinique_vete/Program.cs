using System;
using MySql.Data.MySqlClient;
using clinique_vete.cliniquevt;
using System.Collections;
using System.Text.RegularExpressions;



//Animals animal1 = new Animals();
SQLDATA mysqldata = new SQLDATA();
List<Animals> listanimal = new List<Animals>();

selectchoice();

void affichermenu() //menu principal
{
    Console.WriteLine("1- ajouter un animal");
    Console.WriteLine("2- liste de tous les animaux en pension");
    Console.WriteLine("3- liste des proprietaire");
    Console.WriteLine("4- le nombre total d’animaux en pension ");
    Console.WriteLine("5- le poids total de tous les animaux en pension");
    Console.WriteLine("6- liste des animaux d’une couleur ");
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
    Console.WriteLine("ajouter un animal: ");

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

    } while (validationstring(animal1.nomanimal) || animal1.typeanimal == "");

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
            Console.WriteLine("choix non valide");
        }

    } while (!isdecimal);
    animal1.poidanimal = poids;

    //insert couleur animal
    do
    {
        Console.WriteLine("Veuillez saisir la couleur de l'animal (rouge, violet,bleu): ");
        animal1.couleuranimal = Console.ReadLine();

    } while ((animal1.couleuranimal != "rouge") && (animal1.couleuranimal != "bleu") && (animal1.couleuranimal != "violet") && validationstring(animal1.couleuranimal));

    // insert proprietaire
    do
    {
        Console.WriteLine("Veuillez saisir le proprietaire de l'animal: ");
        animal1.propanimal = Console.ReadLine(); 

    } while (validationstring(animal1.propanimal));

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
    Console.WriteLine("requete update termine ");
    Console.ReadKey();
    Console.Clear();
}


void tableau()  // Voir la liste de tous les animaux en pension (option 2)
{
    Console.WriteLine("liste de tous les animaux en pension: ");
    Console.WriteLine("--------------------------------------------------------------------------");
    Console.WriteLine("| ID " +"\t"+ "| TYPE ANIMAL" + "\t" + "| NOM" +"\t"+ "| AGE" + "\t" + "| POIDS" + "\t" + "| COULEUR" + "  " + "| PROPRIETAIRE");
    Console.WriteLine("--------------------------------------------------------------------------");

    MySqlConnection conn = mysqldata.connectTobase();
    MySqlCommand command = new MySqlCommand("select * from animal", conn);

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
                           "| " + animal1.nomanimal +  "\t" +
                           "| " + animal1.ageanimal +  "\t" +
                           "| " + animal1.poidanimal + "\t" +
                           "| " + animal1.couleuranimal + "\t" +"   "+
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
    Console.WriteLine("le poids total de tous les animaux en pension: ");
    int sum = 0;

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
    Console.WriteLine("liste des animaux d’une couleur: ");
    
    do
    {
        Console.WriteLine("veuillez saisir la couleur de recherche:   ");
        animal1.couleuranimal = Console.ReadLine();

    } while (animal1.couleuranimal != "rouge" && animal1.couleuranimal != "violet" && animal1.couleuranimal != "bleu");

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
    int IDtoremove;
    bool isValide;
    do
    {
        Console.WriteLine("inserer le ID de l'animal que vous voulez retirer de la liste: ");
        isValide = int.TryParse(Console.ReadLine(), out IDtoremove);
        if (isValide == false)
        {
            Console.WriteLine("choix non valide");
        }
    } while (!isValide);
   
    MySqlConnection conn = mysqldata.connectTobase();
    MySqlCommand command = new MySqlCommand("delete from animal where id = @id", conn);
    command.Parameters.AddWithValue("@id", IDtoremove);
    command.ExecuteReader();
    conn.Close();
    tableau();
    Console.ReadKey();
    Console.Clear();
}

void modifierlist()
{
    Animals animal1 = new Animals();
   

    int IDamodifier;
    bool isValide;
    do
    {
        Console.WriteLine("Numero de l'animal a modifier: ");
        isValide = int.TryParse(Console.ReadLine(), out IDamodifier);
        if (isValide == false)
        {
            Console.WriteLine("choix non valide");
        }

    } while (!isValide);


    //int IDamodifier = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Nouveau nom de l'animal: ");

    animal1.nomanimal = Console.ReadLine();
    MySqlConnection conn = mysqldata.connectTobase();
    MySqlCommand command = new MySqlCommand("update animal set nom = @nom where id = @id", conn);
    command.Parameters.AddWithValue("@nom", animal1.nomanimal);
    command.Parameters.AddWithValue("@id", IDamodifier);
    command.ExecuteReader();
    conn.Close();
    Console.WriteLine("le nom du pensionnaire" + " '" + IDamodifier + "' " + "a ete modifie.");
    Console.ReadLine();
    Console.Clear();
}


 bool validationstring(string str)
{
    bool valide = false;
    if (!Regex.Match(str, "^[a-zA-Z]*$").Success)
    {
        valide = true;
        Console.WriteLine("choix non valid");
    }
    return valide;
}

bool validationint(int inputint)
{
    bool valide=false;
    if (!(inputint > 0 && inputint < 100))
    {
        valide = true;
        Console.WriteLine("choix non valid");
    }
    return valide;
}














