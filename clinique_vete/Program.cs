
string[,] animauxtab = new string[10, 7];
affichermenu();

void affichermenu() //menu principal
{
    int option = 0;

    do
    {
        Console.WriteLine("1- ajouter un animal");
        Console.WriteLine("2- liste de tous les animaux en pension");
        Console.WriteLine("3- liste des proprietaire");
        Console.WriteLine("4- le nombre total d’animaux en pension ");
        Console.WriteLine("5- le poids total de tous les animaux en pension");
        Console.WriteLine("6- liste des animaux d’une couleur ");
        Console.WriteLine("7- Retirer un animal de la liste");
        Console.WriteLine("8- Quitter");
        Console.WriteLine("Choisir une option");

        string option2 = Console.ReadLine();
        switch (option2)
        {
            case "1":
                addanimal();
                Console.Clear();
                break;

            case "2":
                tableau();
                Console.ReadLine();
                Console.Clear();
                break;

            case "3":
                proprietaire();
                Console.ReadLine();
                Console.Clear();
                break;

            case "4":
                sommetab();
                Console.ReadLine();
                Console.Clear();
                break;

            case "5":
                poidstotal();
                Console.ReadLine();
                Console.Clear();
                break;

            case "6":
                coloranimal();
                Console.ReadLine();
                Console.Clear();
                break;

            case "7":
                deleteanimal();
                Console.ReadLine();
                Console.Clear();
                break;

            case "8":
                Quitter();
                break;

            default:
                {
                    Console.WriteLine("Le choix n'est pas valide...");
                    break;
                }
        }

    } while (option > 8 || option < 1);

}


void addanimal() // Ajouter un animal (option 1)
{
    Console.WriteLine("ajouter un animal: ");

    Console.WriteLine("Veuillez saisir le type de l'animal: ");
    string typeanimal = Console.ReadLine();

    Console.WriteLine("Veuillez saisir le nom de l'animal: ");
    string nomanimal = Console.ReadLine();

    Console.WriteLine("Veuillez saisir l'age de l'animal: ");
    int ageanimal = int.Parse(Console.ReadLine());

    Console.WriteLine("Veuillez saisir le poids de l'animal: ");
    int poidanimal = int.Parse(Console.ReadLine());

    string couleuranimal;
    do
    {
        Console.WriteLine("Veuillez saisir la couleur de l'animal (rouge, violet,bleu): ");
        couleuranimal = Console.ReadLine();

    } while ((couleuranimal != "rouge") && (couleuranimal != "bleu") && (couleuranimal != "violet"));
    
    Console.WriteLine("Veuillez saisir le proprietaire de l'animal: ");
    string propanimal = Console.ReadLine();
   
    for (int i = 0; i < 10; i++) 
    {
        if (animauxtab[i,0] == null)
        {
            
            animauxtab[i,0] = Convert.ToString(i);
            animauxtab[i, 1] = typeanimal;
            animauxtab[i, 2] = nomanimal;
            animauxtab[i, 3] = Convert.ToString(ageanimal);
            animauxtab[i, 4] = Convert.ToString(poidanimal);
            animauxtab[i, 5] = couleuranimal;
            animauxtab[i, 6] = propanimal;

            break;
        }

    }

}

void tableau()  // Voir la liste de tous les animaux en pension (option 2)
{
    Console.WriteLine("liste de tous les animaux en pension: ");
    Console.WriteLine("--------------------------------------------------------------------------");
    Console.WriteLine("| ID   | TYPE ANIMAL |  NOM  |  AGE  | POIDS  |  COULEUR  |  PROPRIETAIRE |");
    Console.WriteLine("--------------------------------------------------------------------------");

    for (int i = 0; i < 10; i++)
    {
        if (animauxtab[i, 0] != null)
        {
            Console.WriteLine("  " + animauxtab[i, 0]
                                   + "        " + animauxtab[i, 1]
                                   + "        " + animauxtab[i, 2]
                                   + "       " + animauxtab[i, 3]
                                   + "        " + animauxtab[i, 4]
                                   + "      " + animauxtab[i, 5]
                                   + "       " + animauxtab[i, 6]);

        }

    }

}

void proprietaire()  //Voir la liste de tous les propriétaires (option 3)
{
    Console.WriteLine("Dans la fonction voir la liste animaux pension  ");
    Console.WriteLine("--------------------------------------------------------------------------");
    Console.WriteLine("| PROPRIETAIRE |");
    Console.WriteLine("--------------------------------------------------------------------------");

    for (int i = 0; i < 10; i++)
    {
        if (animauxtab[i, 0] != null)
        {
            Console.WriteLine(animauxtab[i, 6]);

        }

    }

}

void sommetab() // Voir le nombre total d’animaux en pension (option 4)
{
    Console.WriteLine("le nombre total d’animaux en pension: ");
    int j = 0;
    int total = 0;
    Console.WriteLine("--------------------------------------------------------------------------");
    Console.WriteLine("| NOMBRE ANIMAUX |");
    Console.WriteLine("--------------------------------------------------------------------------");

    for (int i = 0; i < 10; i++)
    {
        if (animauxtab[i, 0] != null)
        {
            total = int.Parse(animauxtab[i,j])+1;

        }

    }
    Console.WriteLine(total);

}

void poidstotal() // Voir le poids total de tous les animaux en pension (option 5)
{
    Console.WriteLine("le poids total de tous les animaux en pension: ");
    int sum = 0;
   
    Console.WriteLine("--------------------------------------------------------------------------");
    Console.WriteLine("| POIDS TOTALE |");
    Console.WriteLine("--------------------------------------------------------------------------");

    for (int i = 0; i < 10; i++)
    {
        if (animauxtab[i, 0] != null)
        {
            sum += int.Parse(animauxtab[i, 4]);
        }
    }

    Console.WriteLine(sum);
  
}

void coloranimal() //Voir la liste des animaux selon la couleur (option 6)
{
    Console.WriteLine("liste des animaux d’une couleur: ");
    string couleur = "vert";

    do
    {
        Console.WriteLine("veuillez saisir la couleur de recherche:   ");
        couleur = Console.ReadLine();

    } while (couleur != "rouge" && couleur != "violet" && couleur != "bleu");

    Console.WriteLine("--------------------------------------------------------------------------");
    Console.WriteLine("| ID   | TYPE ANIMAL |  NOM  |  COULEUR  |");
    Console.WriteLine("--------------------------------------------------------------------------");

    for (int i = 0; i < 10; i++)
    {
        if (animauxtab[i, 5] == couleur)
        {

            Console.WriteLine("  " + animauxtab[i, 0]
                      + "        " + animauxtab[i, 1]
                      + "        " + animauxtab[i, 2]
                        + "      " + animauxtab[i, 5]);
            
        }
    }
}

void deleteanimal() //Retirer un animal de la liste (option 7)
{

    Console.WriteLine("inserer le ID de l'animal que vous voulez retirer de la liste: ");

    int IDtoremove = Convert.ToInt32(Console.ReadLine());
    int j;
    for (j = 0; j < 7; j++)
    {
        animauxtab[IDtoremove, j] = null;

    }

    //afficher modification tableau 

    tableau();
}

void Quitter() // (option 8)
{
    Environment.Exit(0);
}













