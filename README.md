# WPF-shipping-inventory-demo
This is a C# application with a WPF as the user interface that I built for a school project.  Users can Create, Read, Update, and Delete shipping units.  Searching for all shipping by location is available by drop down menu.

## Description
A shipping company receives packages at its headquarters, which functions as its shipping hub. After receiving the packages the company ships them to a distribution center in one of the following states: Alabama, Florida, Georgia, Kentucky, Mississippi, North Carolina, South Carolina, Tennessee, West Virginia or Virginia. The company needs an application to track the packages that pass through its shipping hub. The application generates a package ID number for each package that arrives at the shipping hub, when the user clicks the application’s Scan New Button. Once a package has been scanned, the user should be able to enter the shipping address for the package. The user should be able to navigate through the list of scanned packages by using <BACK or NEXT> Buttons and by viewing a list of all packages destined for a particular state.

## Getting Started
Download or clone the repository. Run the file lab6.sln

## Description of Project Files
MainWindow.xaml.cs- Controls the main functionality and logic of the user interface.

MainWindow.xaml- User interface of the application.

Packages.cs- This file represents the Packages objects for the application.  

## Testing
Unit tests were performed on a variety of edge cases for packages.  

## Demonstration
Here is an example of the application running.
![alt text][logo]

[logo]: https://github.com/ChrisToplikar/WPF-shipping-inventory-demo/blob/master/demo.JPG?raw=true
 "Application Demo"
