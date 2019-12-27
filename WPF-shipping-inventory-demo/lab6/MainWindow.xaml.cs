using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Package> AL = new List<Package>();
        List<Package> FL = new List<Package>();
        List<Package> GA = new List<Package>();
        List<Package> KY = new List<Package>();
        List<Package> MS = new List<Package>();
        List<Package> NC = new List<Package>();
        List<Package> SC = new List<Package>();
        List<Package> TN = new List<Package>();
        List<Package> WV = new List<Package>();
        List<Package> VA = new List<Package>();
        List<Package> TotalList = new List<Package>();
        List<Package> CurrentPackages = new List<Package>();
        int pos = -1;
        int pos2 = -1;





        public MainWindow()
        {
            InitializeComponent();
            initializeBoxes();


        }

        private void scanButtonClick(object sender, RoutedEventArgs e)
        {//The scan button creates a new package object and displays it to the user for editing
            Package p1 = new Package();
            boxArrivedAt.Text = $"Package: {p1.packageID} on {p1.Date}";
            boxPackageID.Text = p1.packageID.ToString();
            TotalList.Add(p1);
            enableAddButton();
            enableMenu();
        }

        private void backButtonClick(object sender, RoutedEventArgs e)
        {//The back button displays packages in descending order by state selected.
            try
            {
                back();
                
            }
            catch
            {
                List<Package> theList = findListSelectedByComboBox();
                int count = theList.Count;
                if (pos == -1)
                {
                    pos = count - 1;
                }
                if (pos > (count - 1))
                {
                    pos = 0;
                }
                //display last package in list
                Package pk = theList[pos];
                displayPackage(pk);
                pos--;
                enableMenu();
                enablePackageID();

            }
      

        }

        private void nextButtonClick(object sender, RoutedEventArgs e)
        {//The next button displays states in ascending order of the state that is selected.
            try
            {

                next();
                

            }
            catch
            {
                List<Package> theList2 = findListSelectedByComboBox();
                int count = theList2.Count;

                if (pos == -1)
                {
                    pos = 0;
                }
                //display last package in list
                Package pk2 = theList2[pos];
                displayPackage(pk2);
                pos++;
                if (pos > count - 1)
                {
                    pos = 0;
                }
                enableMenu();
                enablePackageID();

            }
        
        }

        private void addButtonClick(object sender, RoutedEventArgs e)
        {//The add button adds a package to records
            try
            {
                int packageNum = int.Parse(boxPackageID.Text);
                Package p1 = TotalList.Find(pac => pac.packageID == packageNum);
                p1.packageID = int.Parse(boxPackageID.Text);
                p1.address = boxAddress.Text;
                p1.zip = int.Parse(boxZipCode.Text);
                p1.city = boxCity.Text;
                //ToDO add info about State--Then add to State List.
                int selectedState = stateComboBox.SelectedIndex;
                p1.state = (States)selectedState;
                CurrentPackages.Add(p1);

                //correct list

                int listNumber = selectedState;
                changeStateList(listNumber, p1);

                //Updating the ListBox
                packagesSelectedComboBox.SelectedIndex = -1;
                packagesSelectedComboBox.SelectedIndex=listNumber;
                disableAddButton();
                disableMenu();
                
            }
            catch
            {
                boxArrivedAt.Text = "Error. Fill out all fields, Zip must be a number.";
            }
        }

        private void removeButtonClick(object sender, RoutedEventArgs e)
        {//Remove button deletes a package from records
            int packageNum = int.Parse(boxPackageID.Text);
            int currentState = stateComboBox.SelectedIndex;
            Package p1 = TotalList.Find(pac => pac.packageID == packageNum);
            CurrentPackages.Remove(p1);
            removeFromStateList(p1);

            packagesSelectedComboBox.SelectedIndex = -1;
            packagesSelectedComboBox.SelectedIndex = currentState;


        }

        private void editButtonClick(object sender, RoutedEventArgs e)
        {//Edit button allows a user to edit the information of a package
            try
            {
                //finds the id of the package
                int packageNum = int.Parse(boxPackageID.Text);
                //finds package object in list of total packages
                Package p1 = TotalList.Find(pac => pac.packageID == packageNum);
                //removes from list of current Packages
                removeFromStateList(p1);
                //EDIT package fields
                p1.address = boxAddress.Text;//edit address
                p1.zip = int.Parse(boxZipCode.Text);//edit zip
                p1.city = boxCity.Text;//edit city
                p1.packageID = int.Parse(boxPackageID.Text);//edit package ID
                //selects index of box for states
                int selectedState = stateComboBox.SelectedIndex;
                p1.state = (States)selectedState;//edits state
                int currentState = stateComboBox.SelectedIndex;
                //removes package from state list
                CurrentPackages.Remove(p1);
                //add package to new state list
                changeStateList(selectedState, p1);
                //refocus on combo box of state selected
                packagesSelectedComboBox.SelectedIndex = -1;
                packagesSelectedComboBox.SelectedIndex = currentState;
            }
            catch
            {
                boxArrivedAt.Text = "Error. Fill out all fields, Zip must be a number.";
            }
        }
        public void initializeBoxes()
        {//helper function that initializes the values in the combo boxes of the UI
            stateComboBox.Items.Add("AL");
            stateComboBox.Items.Add("FL");
            stateComboBox.Items.Add("GA");
            stateComboBox.Items.Add("KY");
            stateComboBox.Items.Add("MS");
            stateComboBox.Items.Add("NC");
            stateComboBox.Items.Add("SC");
            stateComboBox.Items.Add("TN");
            stateComboBox.Items.Add("WV");
            stateComboBox.Items.Add("VA");


            packagesSelectedComboBox.Items.Add("AL");
            packagesSelectedComboBox.Items.Add("FL");
            packagesSelectedComboBox.Items.Add("GA");
            packagesSelectedComboBox.Items.Add("KY");
            packagesSelectedComboBox.Items.Add("MS");
            packagesSelectedComboBox.Items.Add("NC");
            packagesSelectedComboBox.Items.Add("SC");
            packagesSelectedComboBox.Items.Add("TN");
            packagesSelectedComboBox.Items.Add("WV");
            packagesSelectedComboBox.Items.Add("VA");
        }
        public void changeStateList(int i, Package p1)
        {
            //Helper functon moves a package to a list of a different state
            int listNumber = i;
            switch (listNumber)
            {
                case 0:
                    AL.Add(p1);
                    break;
                case 1:
                    FL.Add(p1);
                    break;
                case 2:
                    GA.Add(p1);
                    break;
                case 3:
                    KY.Add(p1);
                    break;
                case 4:
                    MS.Add(p1);
                    break;
                case 5:
                    NC.Add(p1);
                    break;
                case 6:
                    SC.Add(p1);
                    break;
                case 7:
                    TN.Add(p1);
                    break;
                case 8:
                    WV.Add(p1);
                    break;
                case 9:
                    VA.Add(p1);
                    break;
                default:
                    return;
            }
        }
        public void removeFromStateList(Package p1)
        {
            //Helper function removes package from state list
            int currentState = (int)p1.state;
            switch (currentState)
            {
                case 0:
                    AL.Remove(p1);
                    break;
                case 1:
                    FL.Remove(p1);
                    break;
                case 2:
                    GA.Remove(p1);
                    break;
                case 3:
                    KY.Remove(p1);
                    break;
                case 4:
                    MS.Remove(p1);
                    break;
                case 5:
                    NC.Remove(p1);
                    break;
                case 6:
                    SC.Remove(p1);
                    break;
                case 7:
                    TN.Remove(p1);
                    break;
                case 8:
                    WV.Remove(p1);
                    break;
                case 9:
                    VA.Remove(p1);
                    break;
                default:
                    return;
            }
        }
        private void statesSelectedInsideComboBox(object sender, SelectionChangedEventArgs e)
        {
            //This Function prints the packages IDs to the ListBox
            listBox.Items.Clear();
            int selectedState = packagesSelectedComboBox.SelectedIndex;
            //after selecting state, finds current list, prints list to listbox
            switch (selectedState)
            {
                case 0:
                    foreach (Package p in AL)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 1:
                    foreach (Package p in FL)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 2:
                    foreach (Package p in GA)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 3:
                    foreach (Package p in KY)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 4:
                    foreach (Package p in MS)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 5:
                    foreach (Package p in NC)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 6:
                    foreach (Package p in SC)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 7:
                    foreach (Package p in TN)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 8:
                    foreach (Package p in WV)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                case 9:
                    foreach (Package p in VA)
                    {
                        listBox.Items.Add($"ID {p.packageID}");
                    }
                    break;
                default:
                    return;


            }
        }
        public List<Package> findListSelectedByComboBox()
        {
            //Helper function that creates a list of state combo box
            int selectedState = packagesSelectedComboBox.SelectedIndex;
            switch (selectedState)
            {
                case 0:
                    return AL;
                    break;
                case 1:
                    return FL;
                    break;
                case 2:
                    return GA;
                    break;
                case 3:
                    return KY;
                    break;
                case 4:
                    return MS;
                    break;
                case 5:
                    return NC;
                    break;
                case 6:
                    return SC;
                    break;
                case 7:
                    return TN;
                    break;
                case 8:
                    return WV;
                    break;
                case 9:
                    return VA;
                    break;
                default:
                    return null;
            }
        }
        public void displayPackage(Package pk)
        {
            //Helper function for <Back and Next> buttons
            //Displays package details to page
            boxPackageID.Text = pk.packageID.ToString();
            boxZipCode.Text = pk.zip.ToString();
            boxCity.Text = pk.city;
            boxAddress.Text = pk.address.ToString();
            boxArrivedAt.Text = $"Package ID {pk.packageID} arrived on {pk.Date}";
            stateComboBox.SelectedIndex = (int)pk.state;

        }
        public void updateListBox()
        {
            //this is a helper funuction that updates the list box of current packages
            int selectedState = packagesSelectedComboBox.SelectedIndex;
            packagesSelectedComboBox.SelectedIndex = -1;
            packagesSelectedComboBox.SelectedIndex = selectedState;
        }
        public void disableAddButton()
        {
            //helper function to disable the add button
            addButton.IsEnabled = false;
        }
        public void enableAddButton()
        {
            //helper function to enagble the add function
            addButton.IsEnabled = true;
        }
        public void disableMenu()
        {
            //helper function to disable menu buttons
            boxPackageID.IsEnabled = false;
            boxCity.IsEnabled = false;
            boxAddress.IsEnabled = false;
            boxZipCode.IsEnabled = false;
            stateComboBox.IsEnabled = false;
        }
        public void enableMenu()
        {
            //helper function to enable menu functions
            //boxPackageID.IsEnabled = true;
            boxCity.IsEnabled = true;
            boxAddress.IsEnabled = true;
            boxZipCode.IsEnabled = true;
            stateComboBox.IsEnabled = true;
        }
        public void enablePackageID()
        {
            boxPackageID.IsEnabled = true;
        }
        public void back()
        {
            List<Package> theList = findListSelectedByComboBox();
            int count = theList.Count;
            //Does the current on screen package in the list? If not return last item of list.
            if (pos == -1)
            {
                pos = count - 1;
            }
            //display last package in list
            Package pk = theList[pos];
            displayPackage(pk);
            pos--;
            enableMenu();
            enablePackageID();
        }
        public void next()
        {
            List<Package> theList2 = findListSelectedByComboBox();
            int count = theList2.Count;

            if (pos == -1)
            {
                pos = 0;
            }
            //display last package in list
            Package pk2 = theList2[pos];
            displayPackage(pk2);
            pos++;
            if (pos > count - 1)
            {
                pos = 0;
            }
            enableMenu();
            enablePackageID();
        }
    }
}

