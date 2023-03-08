using eRehber.BusinessLayer.Abstract;
using eRehber.BusinessLayer;
using eRehber.Entities;
using eRehber.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eRehber.BusinessLayer.Results;
using eRehber.Entities.Messages;
using System.Web.Services.Description;

namespace eRehber.BusinessLayer
{
    public class KullaniciManager : ManagerBase<Kullanici>
    {
        public BusinessLayerResult<Kullanici> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();

            res.Result = Find(x => x.KullaniciAdi == data.KullaniciAdi && x.Sifre == data.Sifre);
            if (res.Result != null)
            {
                if (res.Result.durum == false)

                {
                    res.AdError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiş.");
                    //res.AdError(ErrorMessageCode.UserLoginSuccess, "Kullanıcı girişi başarılı.");
                }



            }
            else
            {
                res.AdError(ErrorMessageCode.UserIsNotActive, "Kullanıcı Adı veya şifre yanlış.");

            }

            return res;
        }

        public BusinessLayerResult<Kullanici> FPassword(Kullanici register)
        {
            Kullanici user = Find(k => k.KullaniciAdi == register.KullaniciAdi);
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();
            if (user != null)
            {
                if (user.KullaniciAdi != register.KullaniciAdi)
                {
                    res.AdError(ErrorMessageCode.UserNameAlreadyExists, "Kullanıcı Adı Yanlış.");
                }
                if (user.IlAdi != register.IlAdi)
                {
                    res.AdError(ErrorMessageCode.UserNameAlreadyExists, "Seçilen Şehir Yanlış");
                }
                if (user.Cevap != register.Cevap)
                {
                    res.AdError(ErrorMessageCode.EmailAlreadyExists, "Güvenlik Sorusu Cevabı Yanlış");

                }
            }
            else
            {
                res.AdError(ErrorMessageCode.UserCouldNotFind, "Girilen bilgiler yanlış.");
            }

            res.Result = user;
            return res;
        }
        public BusinessLayerResult<Kullanici> RegisterUser(RegisterViewModel data)
        {
            Kullanici user = Find(x => x.KullaniciAdi == data.KullaniciAdi || x.IlAdi == data.IlAdi);
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();
            if (user != null)
            {
                if (user.KullaniciAdi == data.KullaniciAdi)
                {
                    res.AdError(ErrorMessageCode.UserNameAlreadyExists, "Kullanıcı Adı kayıtlı.");
                }
                if (user.IlAdi == data.IlAdi)
                {
                    res.AdError(ErrorMessageCode.EmailAlreadyExists, "Seçili İl J.K'nın kayıtlı kullanıcısı mevcut.");

                }
            }
            else
            {
                int dbResult = base.Insert(new Kullanici()

                {
                    KullaniciAdi = data.KullaniciAdi,
                    Sifre = data.Sifre,
                    Yonetim = false,
                    durum=false,
                    GuvenlikSorusu=data.Guvenlik,
                    Cevap=data.Cevap,
                    IlAdi=data.IlAdi
                   


                });
                if (dbResult > 0)
                {
                    res.Result = Find(x => x.KullaniciAdi == data.KullaniciAdi && x.Sifre == data.Sifre&& x.IlAdi==data.IlAdi);

              
                }
            }
            return res;
        }
    }
}
