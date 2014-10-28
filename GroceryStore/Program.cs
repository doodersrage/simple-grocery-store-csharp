using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore
{
    class Program
    {
        static void Main(string[] args)
        {
            string inpVal = "a";
            int selVal = 0;

            // init classes
            Products aProducts = new Products();
            GroceryManager aGroceryManager = new GroceryManager();
            Cart aCart = new Cart();

            // primary menu loop
            while (inpVal.ToUpper() != "X")
            {
                // reload initial list of products
                aProducts.retrieveProducts();

                Console.Clear(); 
                Console.WriteLine("Select an inventory management option:");
                Console.WriteLine("1. Shop and print order invoice");
                Console.WriteLine("2. Manage product inventory");
                Console.WriteLine("x. exit the program");

                // gather user input
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selVal);

                while (inpVal.ToUpper() != "X" && (selVal > 2 || selVal < 1))
                {
                    Console.WriteLine("Selected values appears to be invalid. Please try again!");
                    inpVal = Console.ReadLine();
                    int.TryParse(inpVal, out selVal);
                }

                // if user did not choose to exit
                if (inpVal.ToUpper() != "X")
                {
                    // apply users selected action
                    switch (selVal)
                    {
                        // create new product
                        case 1:
                            aCart.printMenu();
                            break;
                        // update existing product
                        case 2:
                            aGroceryManager.printMenu();
                            break;
                    }
                }

            }

            Console.WriteLine("Thank you for visiting!");
            Console.ReadLine();
        }
    }
}
