using eRehber.DataAccessLayer.EntityFrameWork;
using eRehber.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRehber.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            for (int i = 0; i < 16; i++)
            {
                Sube sube = new Sube()
                {
                    Ad = FakeData.NameData.GetCompanyName()
                };
                context.Sube.Add(sube);
            }
  
            //for (int i = 0; i < 10; i++)
            //{
            //    Personel personel = new Personel()
            //    {
            //        Ad = FakeData.NameData.GetFirstName(),
            //        Soyad = FakeData.NameData.GetSurname(),
            //        Rutbe = FakeData.NameData.GetCompanyName(),
            //        Gorev = FakeData.NetworkData.GetIpAddress(),
            //        DahiliNo = Convert.ToString(FakeData.PlaceData.GetPostCode()),
            //        Durum = true,
            //        IlAdi = FakeData.PlaceData.GetCity()

            //    };
            //    context.Personel.Add(personel);
            //}
            context.SaveChanges();
        }
    }
}
