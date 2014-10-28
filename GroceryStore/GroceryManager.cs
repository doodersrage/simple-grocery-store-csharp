// manages product inventory
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore
{
    class GroceryManager
    {

        public void printMenu()
        {
            string inpVal = "a";
            int selVal = 0;

            // primary menu loop
            while (inpVal.ToUpper() != "X")
            {
                Console.Clear();
                Console.WriteLine("Current products in inventory:");
                Products.printProducts();
                Console.WriteLine("");
                Console.WriteLine("Select an inventory management option:");
                Console.WriteLine("1. Add new product");
                Console.WriteLine("2. Update existing product");
                Console.WriteLine("3. Remove existing product");
                Console.WriteLine("x. Back to main menu");

                // gather user input
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selVal);

                while (inpVal.ToUpper() != "X" && (selVal > 3 || selVal < 1))
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
                            newProduct();
                            break;
                        // update existing product
                        case 2:
                            updateProduct();
                            break;
                        // delete existing product
                        case 3:
                            removeProduct();
                            break;
                    }
                }
            }

        }
        
        // new product
        private void newProduct()
        {
            string name, qtyStr, priceStr;
            int qty = 0;
            double price = 0;
            bool valid = false;

            // gather product data
            while (valid == false)
            {

                Console.Clear(); 
                Console.Write("Enter a product name: ");
                name = Console.ReadLine();

                while (qty == 0)
                {
                    Console.Write("Enter a product quantity: ");
                    qtyStr = Console.ReadLine();
                    int.TryParse(qtyStr, out qty);
                }

                while (price == 0)
                {
                    Console.Write("Enter a product price: ");
                    priceStr = Console.ReadLine();
                    double.TryParse(priceStr, out price);
                }

                // save product data to list
                Products newProduct = new Products();

                // assign to struct
                valid = newProduct.addProduct(name, qty, price);

                // check for valid product then warn user
                if (valid == false)
                {
                    Console.WriteLine("Product entered appears to be invalid!");
                }
            }

        }

        // update product
        private void updateProduct()
        {
            // set vars
            string inpVal;
            int selVal = 0;
            
            // load product class
            Products newProducts = new Products();

            Console.Clear();
            Console.WriteLine("Edit product in inventory:");
            
            // print list of products
            Products.printProducts();

            Console.WriteLine("Select the product you would like to edit: (x to cancel) ");

            inpVal = Console.ReadLine();
            int.TryParse(inpVal, out selVal);

            // gather user selection
            while (inpVal.ToUpper() != "X" && (selVal > Products.getListCnt() || selVal < 1))
            {
                Console.WriteLine("Selected values appears to be invalid. Please try again!");
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selVal);
            }

            // if user selected a valid product update it
            if (inpVal.ToUpper() != "X")
                newProducts.updateProduct(selVal);
        }

        // remove product
        private void removeProduct()
        {
            // set vars
            string inpVal;
            int selVal = 0;

            // load product class
            Products newProducts = new Products();

            Console.Clear();
            Console.WriteLine("Remove product from inventory:");

            // print list of products
            Products.printProducts();
            Console.WriteLine("Select the product you would like to remove: (x to cancel) ");
            inpVal = Console.ReadLine();
            int.TryParse(inpVal, out selVal);

            // gather user selection
            while (inpVal.ToUpper() != "X" && (selVal > Products.getListCnt() || selVal < 1))
            {
                Console.WriteLine("Selected values appears to be invalid. Please try again!");
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selVal);
            }

            // if user selected a valid product update it
            if (inpVal.ToUpper() != "X")
                newProducts.removeProduct(selVal);

        }
    }
}
