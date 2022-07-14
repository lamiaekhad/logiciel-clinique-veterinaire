using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinique_vete.cliniquevt
{
    internal class insert
    {
        Animals animal1 = new Animals();
        validation validation = new validation();

        public void insererinfo(Animals animal1)
        {
            // insert type animal
            do
            {
                Console.WriteLine("Veuillez saisir le type de l'animal: ");
                animal1.typeanimal = Console.ReadLine();

            } while (validation.validationstring(animal1.typeanimal) || animal1.typeanimal == "");

            //insert nom animal
            do
            {
                Console.WriteLine("Veuillez saisir le nom de l'animal: ");
                animal1.nomanimal = Console.ReadLine();

            } while (validation.validationstring(animal1.nomanimal) || animal1.nomanimal == "");

            // insert age animal
            bool isInt;
            int age;
            do
            {
                Console.WriteLine("Veuillez saisir l'age de l'animal: ");
                isInt = int.TryParse(Console.ReadLine(), out age);

            } while (validation.validationint(age) || !isInt);
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
                animal1.couleuranimal = Console.ReadLine().ToLower();

            } while ((animal1.couleuranimal != "rouge") && (animal1.couleuranimal != "bleu") && (animal1.couleuranimal != "violet") || validation.validationstring(animal1.couleuranimal));

            // insert proprietaire
            do
            {
                Console.WriteLine("Veuillez saisir le proprietaire de l'animal: ");
                animal1.propanimal = Console.ReadLine();

            } while (validation.validationstring(animal1.propanimal) || animal1.propanimal == "");

        }



    }
}
