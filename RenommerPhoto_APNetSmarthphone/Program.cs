using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;


namespace RenommerPhoto_APNetSmarthphone
{
    class Program
    {
        static void Main(string[] args)
        {
            ListeFichiersPhotos listeFichiersPhotos = ListeFichiersPhotos.GetInstance();
            listeFichiersPhotos.Fabriquer();
            listeFichiersPhotos.Sauvegarder();
        }


    }
}
