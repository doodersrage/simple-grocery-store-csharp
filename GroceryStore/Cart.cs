// manages users shopping cart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace GroceryStore
{
    class Cart
    {

        // cart structure
        public struct CartItm
        {
            public int id;
            public int qty;
        }

        // totals properties
        private double subTotal;
        private double tax = 0.06;
        private double total;
        
        // products list
        public static List<CartItm> cart = new List<CartItm>();

        public int getListCnt()
        {
            return cart.Count;
        }

        // print user menu
        public void printMenu()
        {
            string inpVal = "a";
            int selVal;

            // primary menu loop
            while(inpVal.ToUpper() != "X"){
                Console.Clear();
                listItems();
                Console.WriteLine("");
                Console.WriteLine("Select an inventory management option:");
                Console.WriteLine("1. Add product to cart");
                Console.WriteLine("2. Update existing product quantity");
                Console.WriteLine("3. Remove existing product from cart");
                Console.WriteLine("4. Print invoice");
                Console.WriteLine("x. Back to main menu");
            
                // gather user input
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selVal);

                while (inpVal.ToUpper() != "X" && Convert.ToInt32(inpVal) > 3 && Convert.ToInt32(inpVal) < 1)
                {
                    Console.WriteLine("Selected values appears to be invalid. Please try again!");
                    inpVal = Console.ReadLine();
                }

                // if user did not choose to exit
                if(inpVal.ToUpper() != "X"){
                    selVal = Convert.ToInt32(inpVal);
                    // apply users selected action
                    switch(selVal){
                            // create new product
                        case 1:
                            addProductChoice();
                            break;
                            // update existing product
                        case 2:
                            updateCartItem();
                            break;
                            // delete existing product
                        case 3:
                            removeFromCart();
                            break;
                        case 4:
                            printInvoice();
                            break;
                    }
                }

            }

        }

        // add product to shopping cart dilogue
        private void addProductChoice(){
            string inpVal;
            int selVal;

            // print list of products
            Console.Clear();
            Console.Write("Add product to shopping cart:");
            Products.printProducts();
            Console.Write("Please enter the id number for the product you would like to add to your shopping cart: (x to cancel) ");

            inpVal = Console.ReadLine();
            int.TryParse(inpVal, out selVal);

            while(inpVal.ToUpper() != "X" && (selVal < 1 || selVal > Products.getListCnt())){
                Console.WriteLine("Your selected value did not match any available product options. Please try again: ");
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selVal);
            }

            if(inpVal.ToUpper() != "X")
                addToCart(selVal);

        }

        // add product to users cart
        private void addToCart(int id)
        {
            string inpVal;
            int qty;
            Products.Product prodData;

            // update to valid id
            --id;

            Console.Clear();

            // load product data
            prodData = Products.getProduct(id);

            // new item structure
            CartItm newCartItm = new CartItm();

            newCartItm.id = id;

            // promp user for quantity
            Console.Write("How many should I add to your cart? ");

            inpVal = Console.ReadLine();
            int.TryParse(inpVal, out qty);

            // check user inserted quantity for validity
            while (qty <= 0 && qty < prodData.qty)
            {
                Console.Write("You did not enter a valid quantity. Please try again: ");
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out qty);
            }

            newCartItm.qty = qty;

            // check for item already in cart
            for (int i = 0; i < cart.Count; ++i)
            {
                if (cart[i].id == id)
                {
                    updateCartItem(id, (cart[i].qty + qty));
                    return;
                }
            }
            // save item to cart
            cart.Add(newCartItm);
        }

        // update item in cart dialogue
        private void updateCartItem(){
            string inpVal;
            int selVal, selQty;
            Products.Product prodData;

            Console.Clear();
            Console.WriteLine("Cart Editor");
            Console.WriteLine("Current items and quantities in shopping cart.");
            listItems();
            Console.WriteLine("");
            Console.WriteLine("Please select a cart item you would like to edit by entering its id value : (x to cancel)");

            inpVal = Console.ReadLine();
            int.TryParse(inpVal, out selVal);

            while(inpVal.ToUpper() != "X" && (selVal < 1 || selVal > cart.Count)){
                Console.WriteLine("Your selected value did not match any available product options. Please try again: ");
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selVal);
            }

            if(inpVal.ToUpper() != "X"){
                // decrement selected id val to match list id
                --selVal;

                // load product data
                prodData = Products.getProduct(selVal);

                Console.Write("Please enter a new quantity for {0} : ", prodData.name);
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selQty);
                while (selQty < 1 || selQty > prodData.qty)
                {
                    Console.WriteLine("Entered quantity appears to be invalid. Please try again: ");
                    inpVal = Console.ReadLine();
                    int.TryParse(inpVal, out selQty);
                }
                // reset cart item quantity
                CartItm aCartItem = new CartItm();
                aCartItem.id = cart[selVal].id;
                aCartItem.qty = selQty;
                cart[selVal] = aCartItem;
            }
        }
        // update cart item quantity without prompt
        private void updateCartItem(int id, int qty)
        {
            CartItm aCartItem = new CartItm();
            aCartItem.id = cart[id].id;
            aCartItem.qty = qty;
            cart[id] = aCartItem;
        }

        // remove product from users cart
        private void removeFromCart(){
            string inpVal;
            int selVal;

            Console.Clear();
            Console.WriteLine("Cart Editor");
            Console.WriteLine("Current items and quantities in shopping cart.");
            listItems();
            Console.WriteLine("");
            Console.WriteLine("Please select a cart item you would like to edit by entering its id value : (x to cancel)");

            inpVal = Console.ReadLine();
            int.TryParse(inpVal, out selVal);

            while (inpVal.ToUpper() != "X" && (selVal < 1 || selVal > cart.Count))
            {
                Console.WriteLine("Your selected value did not match any available product options. Please try again: ");
                inpVal = Console.ReadLine();
                int.TryParse(inpVal, out selVal);
            }

            if (inpVal.ToUpper() != "X")
                removeItemFromCart(selVal);
        }
        // remove product from users cart without prompt
        private void removeItemFromCart(int id)
        {
            --id;
            cart.RemoveAt(id);
        }

        // list products in cart
        private void listItems()
        {
            double prodPrice;
            Products.Product prodData;

            // reset total values
            subTotal = 0;
            total = 0;

            if (cart.Count > 0)
            {
                // print list header
                Console.WriteLine("ID  \tName\tPrice\tQty\tTotal");

                for (int i = 0; i < cart.Count; ++i)
                {
                    prodData = Products.getProduct(cart[i].id);
                    prodPrice = (double)prodData.price * cart[i].qty;
                    Console.WriteLine("{0}. {1}\t{2}\t{3}\t{4}", i + 1, prodData.name, prodData.price.ToString("C"), cart[i].qty, prodPrice.ToString("C"));

                    subTotal += prodPrice;
                    Console.WriteLine("Sub-Total: {0}", subTotal.ToString("C"));
                }

                total = subTotal * (tax + 1);
            }
            else
            {
                Console.WriteLine("Your shopping cart appears to be empty.");
            }
        }

        // print invoice
        private void printInvoice()
        {
            double prodPrice;
            Products.Product prodData;
            Products aProduct = new Products();

            Console.Clear();
            Console.WriteLine("Items in cart and totals as listed below: ");

            listItems();

            Console.WriteLine("Tax: {0,11}", tax.ToString("C"));
            Console.WriteLine("Total: {0,10}", total.ToString("C"));

            // save results to txt document
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            double unixTime = span.TotalSeconds;
            string invoicePath = string.Format("invoice-{0}.txt", unixTime);

            string path = Path.GetDirectoryName(Application.ExecutablePath);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path.Combine(path, invoicePath)))
            {

                var newLine = string.Format("ID  \tName\tPrice\tQty\tTotal");
                file.WriteLine(newLine);

                // loop through list products
                for (int i = 0; i < cart.Count; ++i)
                {

                    // add file line
                    prodData = Products.getProduct(cart[i].id);
                    prodPrice = (double)prodData.price * cart[i].qty;
                    newLine = string.Format("{0}. {1}\t{2}\t{3}\t{4}", i, prodData.name, prodData.price.ToString("C"), cart[i].qty, prodPrice.ToString("C"));
                    file.WriteLine(newLine);

                    // update products quantity
                    aProduct.updateProductQty(cart[i].id, (prodData.qty - cart[i].qty));

                }

                // save totals
                newLine = string.Format("Sub-Total: {0}", subTotal.ToString("C"));
                file.WriteLine(newLine);

                newLine = string.Format("Tax: {0,11}", tax.ToString("C"));
                file.WriteLine(newLine);

                newLine = string.Format("Total: {0,10}", total.ToString("C"));
                file.WriteLine(newLine);

            }

            // clear cart
            cart.Clear();

            // open newly generated file
            System.Diagnostics.Process.Start(Path.Combine(path, invoicePath));

            // pause
            Console.ReadLine();
        }
    }
}
