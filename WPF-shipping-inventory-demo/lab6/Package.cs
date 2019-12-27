using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Package
    {

        public int packageID { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        
        public int zip { get; set; }
        public static int packageCount = 0;
        public States state { get; set; }
        public string Date;

        public Package()
        {
            packageCount++;
            this.packageID = packageCount;
            this.address = "";
            this.city = "";
            this.Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            this.zip = 0;
        }
        public void SetStateLocation(int i)
        {
            this.state = (States)i;
        }


        
    }
    public enum States
    {
        AL=0,
        FL=1,
        GA=2,
        KY=3,
        MS=4,
        NC=5,
        SC=6,
        TN=7,
        WV=8,
        VA=9,
    }
}
