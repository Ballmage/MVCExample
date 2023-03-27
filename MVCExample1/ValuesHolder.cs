using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;


namespace MVCExample1
{
    public class ValuesHolder
    {

        int a, b, c;
        public Dictionary<DateTime, int> Values = new Dictionary<DateTime, int>()
        {
            [new DateTime(2015, 5, 18)] = 35,
            [new DateTime(2015, 5, 19)] = 36,
            [new DateTime(2015, 5, 20)] = 35,
            [new DateTime(2015, 5, 21)] = 37

        };
        public string GetTempBtwDate(DateTime datefrom, DateTime dateto)
        {
           
          
            DateTime[] arrdate = new DateTime[Values.Count];
            Values.Keys.CopyTo(arrdate, 0);
            
            
            

            for (int i = 0; i < arrdate.Length; i++)
            {

                if (arrdate[i] == datefrom)
                {
                    a = i;
                }
                else if (arrdate[i] == dateto)
                {
                    b = i;
                }

            }

           List<string>  temp = new List<string>();
            

            for (int i = a+1; i < b; i++)
            {
                temp.Add(Convert.ToString(Values[arrdate[i]]));
            }


            return temp.ToString();
        }
        public void DelBtw(DateTime datefrom, DateTime dateto)
        {


            DateTime[] arrdate = new DateTime[Values.Count];
            Values.Keys.CopyTo(arrdate, 0);




            for (int i = 0; i < arrdate.Length; i++)
            {

                if (arrdate[i] == datefrom)
                {
                    a = i;
                }
                else if (arrdate[i] == dateto)
                {
                    b = i;
                }

            }

          


            for (int i = a + 1; i < b; i++)
            {
                Values.Remove(arrdate[i]);
            }


            
        }


    }
}
