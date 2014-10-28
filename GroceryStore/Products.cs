// class used for managing products within the grocery store
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace GroceryStore
{
    class Products
    {
        // products constructor
        public Products()
        {

        }

        // product structure
        public struct Product
        {
            public string name;
            public int qty;
            public double price;
        }

        // products list
        public static List<Product> productList = new List<Product>();

        public static int getListCnt()
        {
            return productList.Count;
        }

        // get product from list
        public static Product getProduct(int id)
        {
            return productList[id];
        }

        // CSV to store data
        private string groceriesList = "groceries.csv";

        // add product
        public bool addProduct(string name, int qty, double price)
        {
            // add product to struct
            Product newProduct = new Product();

            // set product values
            newProduct.name = name;
            newProduct.qty = qty;
            newProduct.price = price;

            // check for all product values
            if (newProduct.name != "" && newProduct.qty > 0 && newProduct.qty > 0)
            {
                // add to product list
                productList.Add(newProduct);
                // save updated product
                saveProducts();
                return true;
            }
            else
            {
                Console.WriteLine("Invalid product!");
                return false;
            }

        }

        // update product
        public void updateProduct(int id)
        {
            // set vars
            string strIn, name;
            int qty;
            double price;

            // make id match selected value in list
            --id;

            Console.Clear();
            // promp user to select input
            Console.WriteLine("Which product would you like to update?");
            Console.WriteLine("Name\tQty\tPrice");
            Console.WriteLine("{0}\t{1}\t{2}", productList[id].name, productList[id].qty, productList[id].price.ToString("C"));
            Console.WriteLine("");

            // gather user data
            Console.Write("Enter a product name: (x to keep the same) ");
            strIn = Console.ReadLine();
            if (strIn.ToUpper() != "X")
            {
                name = strIn;
            }
            else
            {
                name = productList[id].name;
            }

            Console.Write("Enter a product quantity: (x to keep the same) ");
            strIn = Console.ReadLine();
            if (strIn.ToUpper() != "X" && int.TryParse(strIn, out qty))
            {
                qty = Convert.ToInt32(strIn);
            }
            else
            {
                qty = productList[id].qty;
            }

            Console.Write("Enter a product price: (x to keep the same) ");
            strIn = Console.ReadLine();
            if (strIn.ToUpper() != "X" && double.TryParse(strIn, out price))
            {
                price = Convert.ToDouble(strIn);
            }
            else
            {
                price = productList[id].price;
            }

            // save updated info to list
            // add product to struct
            Product newProduct = new Product();

            // set product values
            newProduct.name = name;
            newProduct.qty = qty;
            newProduct.price = price;

            // update product from list
            productList[id] = newProduct;

            saveProducts();

        }
        // update product quantity without prompt
        public void updateProductQty(int id, int qty)
        {
            Product aProduct = new Product();
            aProduct = productList[id];
            aProduct.qty = qty;
            productList[id] = aProduct;

            // save changes
            saveProducts();
        }

        // remove product by name
        public void removeProduct(string prodName)
        {
            // find product in list then remove it
            for (int i = 0; i < productList.Count; ++i)
            {
                // remove product from list if found
                if (productList[i].name.ToUpper() == prodName.ToUpper())
                {
                    productList.RemoveAt(i);
                }
            }

            // save changes
            saveProducts();

        }
        // remove product by id
        public void removeProduct(int id)
        {
            // make id match selected value in list
            --id;
            // remove product
            productList.RemoveAt(id);

            // save changes
            saveProducts();
        }

        // print products list
        public static void printProducts()
        {
            if (productList.Count > 0)
            {
                Console.WriteLine("ID  \tName\tQty\tPrice");
                for (int i = 0; i < productList.Count; ++i)
                {
                    Console.WriteLine("{0}. {1}\t{2}\t{3}", i + 1, productList[i].name, productList[i].qty, productList[i].price.ToString("C"));
                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Your inventory appears to be empty!");
            }
        }

        // retrieve products from CSV
        public void retrieveProducts()
        {

            // clear products list
            productList.Clear();
            
            // load saved csv
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            using (TextFieldParser parser = new TextFieldParser(Path.Combine(path, groceriesList)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData) 
                {
                    //Processing row
                    string[] fields = parser.ReadFields();

                    // add product to struct
                    Product newProduct = new Product();

                    // set product values
                    newProduct.name = fields[0];
                    newProduct.qty = Convert.ToInt32(fields[1]);
                    newProduct.price = Convert.ToDouble(fields[2]);

                    // store in class property list
                    productList.Add(newProduct);

                }
            }
        }

        // save products to csv
        public void saveProducts()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(path, groceriesList)))
            {

                // loop through list products
                foreach (Product prod in productList) // Loop through List with foreach
                {

                    // add CSV line
                    var newLine = string.Format("{0},{1},{2}", prod.name, prod.qty, prod.price);
                    file.WriteLine(newLine);

                }

            }
        }
    }
}
