using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using System.Threading;
using OpenQA.Selenium;


//ObjectTest --> Done


namespace SetCRMObjectTest
{
    internal class ObjectTest
    {
        [TestFixture]
        public class SignUpShouldBeWork
        {
            public FirefoxDriver _browser;
            //public DateTime day = DateTime.Now;     
            public int hour = (int)System.DateTime.Now.Hour;
            public int day = (int)System.DateTime.Now.Day;
            public int month = (int)System.DateTime.Now.Month;
            public string mail;

            [SetUp]
            public void Setup()
            {
                _browser = new FirefoxDriver();
            }

            [Test]
            public void signup_for_objecttest()
            {
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/user/new");

                _browser.FindElementById("FirstName").SendKeys("Nezir");
                _browser.FindElementById("LastName").SendKeys("Yurekli");
                _browser.FindElementById("Email").SendKeys("ObjTest" + "-" + month + "-" + day + "@argeset.com");
                _browser.FindElementById("Password").SendKeys("password");
                _browser.FindElementById("CompanyName").SendKeys("Company");
                _browser.FindElementById("IsAcceptedTerms").Click();

                mail = _browser.FindElementById("Email").Text;

                _browser.FindElementById("frm").Submit();

                _browser.Close();
            }
            
            [Test]
            public void signup_for_dialytest()
            {
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/user/new");

                _browser.FindElementById("FirstName").SendKeys("Nezir");
                _browser.FindElementById("LastName").SendKeys("Yurekli");
                _browser.FindElementById("Email").SendKeys(month + "-" + day + "@argeset.com");
                _browser.FindElementById("Password").SendKeys("password");
                _browser.FindElementById("CompanyName").SendKeys("Company");
                _browser.FindElementById("IsAcceptedTerms").Click();

                mail = _browser.FindElementById("Email").Text;

                _browser.FindElementById("frm").Submit();

                _browser.Close();
            }

            [Test]
            public void SignIn_ObjectTest_NoClose()
            {
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/user/login");

                _browser.FindElementById("Email").SendKeys("ObjTest" + "-" + +month + "-" + day + "@argeset.com");
                _browser.FindElementById("Password").SendKeys("password");

                _browser.FindElementById("frm").Submit();
            }

            [Test]
            public void SignIn_DialyTest_NoClose()
            {
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/user/login");

                _browser.FindElementById("Email").SendKeys(month + "-" + day + "@argeset.com");
                _browser.FindElementById("Password").SendKeys("password");

                _browser.FindElementById("frm").Submit();
            }
            
        }

        [TestFixture]
        public class ObjectTests
        {
            public FirefoxDriver _browser;
            public int day = (int)System.DateTime.Now.Day;
            public int month = (int)System.DateTime.Now.Month;
            public int hour = (int)System.DateTime.Now.Hour;
            public string[] userName = new string[] { "Name1", "Name2", "Name3", "Name4" };
            public string[] userSurname = new string[] { "SName1", "SName2", "SName3", "SName4" };
            public string[] email = new string[] { "Mail1", "Mail2", "Mail3", "Mail4" };

            public string[] objectName = new string[] { "FirmaTest", "IlgilikisiTest", "AktiviteTest", "GorevTest", "UrunTest" };

            public string[] requiredName = new string[] { "Firma Adi", "Ad", "Aktivite Açıklaması", "Görev Açıklaması", "Ürün Kodu" };

            [SetUp]
            public void Setup()
            {
                _browser = new FirefoxDriver();
            }

            public void SignIn()
            {
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/user/login");

                _browser.FindElementById("Email").SendKeys("ObjTest" + "-" +month + "-" + day + "@argeset.com");
                _browser.FindElementById("Password").SendKeys("password");

                _browser.FindElementById("frm").Submit();
            }
            
            private void EnsureFindElement(string key)
            {
                var isNotVisible = true;
                while (isNotVisible)
                {
                    try
                    {
                        _browser.FindElement(
                            By.XPath(key
                                ))
                            .Click();

                        isNotVisible = false;
                    }
                    catch (Exception)
                    {
                        isNotVisible = true;
                    }
                }
            }

            //[Test]
            //public void step_01_define_new_user()
            //{
            //    SignIn();

            //    for (int i = 0; i < userName.Length - 1; i++)
            //    {
            //        _browser.Navigate().GoToUrl("http://dev.setcrm.com/company/newuser");

            //        _browser.FindElement(By.Id("FirstName")).SendKeys(userName[i]);
            //        _browser.FindElementById("LastName").SendKeys(userSurname[i]);
            //        _browser.FindElementById("Email").SendKeys(email[i] + "@argeset.com");
            //        _browser.FindElementById("Password").SendKeys("password");

            //        _browser.FindElementById("btn_save_and_new").Click();
            //    }

            //    _browser.Navigate().GoToUrl("dev.setcrm.com/company/newuser");

            //    _browser.FindElementById("FirstName").SendKeys(userName[3]);
            //    _browser.FindElementById("LastName").SendKeys(userSurname[3]);
            //    _browser.FindElementById("Email").SendKeys(email[3] + "@argeset.com");
            //    _browser.FindElementById("Password").SendKeys("password");

            //    _browser.FindElementById("btn_save").Click();

            //    _browser.Close();
            //}

            [Test]
            public void step_02_define_costum_object()
            {
                SignIn();


                for (int i = 0; i < objectName.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customobject/list");

                    _browser.FindElement(By.XPath("//a[@class='btn btn-sm btn-success']")).Click();

                    _browser.FindElement(By.Id("Name")).SendKeys(objectName[i]);
                    _browser.FindElement(By.Id("RequiredFieldName")).SendKeys(requiredName[i]);

                    _browser.FindElement(By.Id("IsActive")).Click();

                    _browser.FindElement(By.Id("btn_save")).Click(); //btn_save_and_new
                }

                _browser.Close();
            }

            [Test]
            public void step_03_CFW_firmaTest()
            {
                string[] textItems = new string[] { "Vergi Dairesi", "Posta Kodu", "Açıklaması" };
                string[] selectlistItems = new string[] { "Firma Türü", "Referans Bilgileri", "Ülke", "İl", "İlçe", "Sektör" };

                SignIn();

                //Text_FirmaTicariAdi -> Required
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");

                _browser.FindElementById("s2id_autogen1_search").SendKeys("FirmaTest");
                
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='1']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementById("Name").SendKeys("Firma Ticari Adı");
                _browser.FindElementById("IsRequired").Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Text Fields
                for (int i = 0; i < textItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("FirmaTest");
                    Thread.Sleep(1000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='1']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementById("Name").SendKeys(textItems[i]);

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                //Vergi Numarası                
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");

                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='3']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Vergi Numarası");
                _browser.FindElementById("IsUnique").Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Firma Email
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("FirmaTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='5']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Firma Email");

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Firma Telefon
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("FirmaTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='6']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementById("Name").SendKeys("Firma Telefon");

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Firma WebSayfası
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("FirmaTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='7']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Web Sayfası");

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                for (int i = 0; i < selectlistItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("FirmaTest");
                    Thread.Sleep(1000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='12']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(2000);

                    _browser.FindElementById("Name").SendKeys(selectlistItems[i]);
                    _browser.FindElementById("SelectListItems").SendKeys("Undefined");

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(2000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                Thread.Sleep(5000);
                _browser.Close();
            }

            [Test]
            public void step_04_CFW_ilgiliKisiTest()
            {
                string[] selectlistIlgilikisiItems = new string[] { "Bölüm", "Ünvan", "Cinsiyet" };

                SignIn();

                //Text Soyad->Required
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("IlgilikisiTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='1']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementById("Name").SendKeys("Soyad");
                _browser.FindElementById("IsRequired").Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Number -> TC No
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("IlgilikisiTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='3']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("TC Kimlik No");
                _browser.FindElement(By.Id("IsUnique")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Email -> Email
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("IlgilikisiTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='5']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("EMail");

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Phone -> GSM
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("IlgilikisiTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='6']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementById("Name").SendKeys("GSM");

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Text -> Acıklama
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("IlgilikisiTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='1']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementById("Name").SendKeys("Açıklama");
                _browser.FindElementById("IsRequired").Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Selectlist -> 
                for (int i = 0; i < selectlistIlgilikisiItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("IlgilikisiTest");
                    Thread.Sleep(3000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='12']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementById("Name").SendKeys(selectlistIlgilikisiItems[i]);
                    _browser.FindElementById("SelectListItems").SendKeys("Undefined");

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                //SystemLookup Müşteri Temsilcisi
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("IlgilikisiTest");
                Thread.Sleep(3000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='14']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElement(By.Id("Name")).SendKeys("Müşteri Temsilcisi");

                _browser.FindElement(By.XPath("//a[@class='wizard-next']")).Click();
                Thread.Sleep(3000);

                _browser.FindElement(By.XPath("//a[@class='wizard-finish']")).Click();

                //LookUp Firma
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("İlgiliKisiTest");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='13']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();

                EnsureFindElement("//a[@class='select2-choice select2-default']");
                Thread.Sleep(1000);
                var getTextid = _browser.FindElements(By.XPath("//input[@class='select2-input select2-focused']"));

                foreach (var item in getTextid)
                {
                    var id = item.GetAttribute("id");
                    _browser.FindElement(By.Id(id)).SendKeys("IlgiliKisiTest");
                }
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.Id("Name")).SendKeys("Firma");

                Thread.Sleep(2000);
                _browser.FindElement(By.XPath("//a[@class='wizard-next']")).Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();

                Thread.Sleep(5000);
                _browser.Close();

            }

            [Test]
            public void step_05_CFW_aktiviteTest()
            {
                string[] selectlistAktiviteItems = new string[] { "Aktivite Türü", "Aktivite Şekli", "Öncelik Durumu" };
                string[] lookUpAktiviteItems = new string[] { "ilgili Firma", "İlgili Kişi" };

                SignIn();
                
                //Lookup
                for (int i = 0; i < lookUpAktiviteItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");
                    
                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("AktiviteTest");
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='13']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();

                    EnsureFindElement("//a[@class='select2-choice select2-default']");
                    Thread.Sleep(1000);
                    var getTextid = _browser.FindElements(By.XPath("//input[@class='select2-input select2-focused']"));

                    foreach (var item in getTextid)
                    {
                        var id = item.GetAttribute("id");
                        _browser.FindElement(By.Id(id)).SendKeys("AktiviteTest");
                    }
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.Id("Name")).SendKeys(lookUpAktiviteItems[i]);

                    Thread.Sleep(2000);
                    _browser.FindElement(By.XPath("//a[@class='wizard-next']")).Click();
                    Thread.Sleep(3000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                //Selectlist
                for (int i = 0; i < selectlistAktiviteItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("AktiviteTest");
                    Thread.Sleep(1000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='12']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementById("Name").SendKeys(selectlistAktiviteItems[i]);
                    _browser.FindElement(By.Id("IsRequired")).Click();
                    _browser.FindElementById("SelectListItems").SendKeys("Undefined");

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                //DateTime Aktivite Tarihi ve Saati
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("AktiviteTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='9']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementById("Name").SendKeys("Aktivite Tarihi ve Saati");
                _browser.FindElementById("IsRequired").Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();

                Thread.Sleep(5000);
                _browser.Close();
            }

            [Test]
            public void step_06_CFW_gorevTest()
            {
                string[] selectlistGorevItems = new string[] { "Görev Türü", "Öncelik", "Görev Durumu" };
                string[] lookUpGorevItems = new string[] { "ilgili Firma", "İlgili Kişi" };
                string[] datetimeGorevItems = new string[] { "Görev Başlangıç Tarihi", "Görev Bitiş Tarihi" };

                SignIn();

                //Lookup
                for (int i = 0; i < lookUpGorevItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("GorevTest");
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='13']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();

                    EnsureFindElement("//a[@class='select2-choice select2-default']");
                    var getTextid = _browser.FindElements(By.XPath("//input[@class='select2-input select2-focused']"));

                    foreach (var item in getTextid)
                    {
                        var id = item.GetAttribute("id");
                        _browser.FindElement(By.Id(id)).SendKeys("GorevTest");
                    }
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.Id("Name")).SendKeys(lookUpGorevItems[i]);

                    Thread.Sleep(2000);
                    _browser.FindElement(By.XPath("//a[@class='wizard-next']")).Click();
                    Thread.Sleep(3000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                //Selectlist
                for (int i = 0; i < selectlistGorevItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("GorevTest");
                    Thread.Sleep(1000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='12']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementById("Name").SendKeys(selectlistGorevItems[i]);
                    _browser.FindElement(By.Id("IsRequired")).Click();
                    _browser.FindElementById("SelectListItems").SendKeys("Undefined");

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }
                
                //SystemLookup Atanan
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("GorevTest");
                Thread.Sleep(1000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='14']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(3000);

                _browser.FindElement(By.Id("Name")).SendKeys("Atanan");

                _browser.FindElement(By.XPath("//a[@class='wizard-next']")).Click();
                Thread.Sleep(3000);

                _browser.FindElement(By.XPath("//a[@class='wizard-finish']")).Click();

                //DateTime 
                for (int i = 0; i < datetimeGorevItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("GorevTest");
                    Thread.Sleep(1000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='9']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementById("Name").SendKeys(datetimeGorevItems[i]);
                    _browser.FindElementById("IsRequired").Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                Thread.Sleep(5000);
                _browser.Close();
            }

           

            [Test]
            public void step_07_CFW_urunTest()
            {
                string[] textUrunItems = new[] { "Ürün Markası", "Ürün Açıklaması", "Ürün Grubu", "Ürün Barkodu" };
                string[] selectListUrunItems = new string[] { "Ürün Birim Seti", "Ürün Fiyat Para Birimi", "Stok Takip Şekli" };
                string[] numberUrunItems = new string[]
                {
                    "Ürün Alış Fiyatı", "Ürün Satış Fiyatı", "Asgari Stok Miktarı", "Minimum Sipariş Miktarı",
                    "Tedarik Süresi(Gün)"
                };
                string[] percentUrunItems = new string[] { "KDV Oranı", "İskonto oranı" };

                SignIn();

                //Lookup
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("UrunTest");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='13']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();

                EnsureFindElement("//a[@class='select2-choice select2-default']");
                var getTextid = _browser.FindElements(By.XPath("//input[@class='select2-input select2-focused']"));

                foreach (var item in getTextid)
                {
                    var id = item.GetAttribute("id");
                    _browser.FindElement(By.Id(id)).SendKeys("UrunTest");
                }
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.Id("Name")).SendKeys("Tedarik Firması");

                Thread.Sleep(2000);
                _browser.FindElement(By.XPath("//a[@class='wizard-next']")).Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();

                //Text
                for (int i = 0; i < textUrunItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");
                    
                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("UrunTest");
                    Thread.Sleep(1000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='1']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(2000);

                    _browser.FindElement(By.Id("Name")).SendKeys(textUrunItems[i]);

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(2000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                //SelectList
                for (int i = 0; i < selectListUrunItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");
                    
                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("UrunTest");
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='12']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementById("Name").SendKeys(selectListUrunItems[i]);
                    _browser.FindElement(By.Id("IsRequired")).Click();
                    _browser.FindElementById("SelectListItems").SendKeys("Undefined");

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(3000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                //Number
                for (int i = 0; i < numberUrunItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("UrunTest");
                    Thread.Sleep(1000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='3']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(2000);

                    _browser.FindElement(By.Id("Name")).SendKeys(numberUrunItems[i]);

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(2000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                //Percent
                for (int i = 0; i < percentUrunItems.Length; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                    EnsureFindElement("//span[@class='select2-arrow']");
                    _browser.FindElementById("s2id_autogen1_search").SendKeys("UrunTest");
                    Thread.Sleep(1000);
                    EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                    _browser.FindElement(By.XPath("//input[@value='8']")).Click();

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(2000);

                    _browser.FindElement(By.Id("Name")).SendKeys(percentUrunItems[i]);

                    _browser.FindElementByClassName("wizard-next").Click();
                    Thread.Sleep(2000);

                    _browser.FindElementByClassName("wizard-finish").Click();
                }

                Thread.Sleep(5000);
                _browser.Close();

            }



            [Test]
            public void step_08_insert_firmatest()
            {
                Random random = new Random();
                var number = random.Next(10000, 1000000);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/custom/new/firmatest");

                //Text
                var fieldTypeText = _browser.FindElements(By.XPath("//div[@data-fieldtype='Text']"));

                foreach (var item in fieldTypeText)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("test");
                }

                //Url
                var fieldTypeUrl = _browser.FindElements(By.XPath("//div[@data-fieldtype='Url']"));

                foreach (var item in fieldTypeUrl)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("http://www.argeset.com");
                }

                //Phone
                var fieldTypePhone = _browser.FindElements(By.XPath("//div[@data-fieldtype='Phone']"));

                foreach (var item in fieldTypePhone)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("121212121212");
                }

                //Email
                var fieldTypeEmail = _browser.FindElements(By.XPath("//div[@data-fieldtype='Email']"));

                foreach (var item in fieldTypeEmail)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("sefa@argeset.com");
                }

                //Number -> Vergi Numarası
                var fieldTypeNumber = _browser.FindElements(By.XPath("//div[@data-fieldtype='Number']"));

                foreach (var item in fieldTypeNumber)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("" + number);
                }

                //SelectList -> Firma Türü
                EnsureFindElement("//span[@id='select2-chosen-1']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Referans Bilgileri
                EnsureFindElement("//span[@id='select2-chosen-2']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Ülke
                EnsureFindElement("//span[@id='select2-chosen-3']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //Save and close browser
                _browser.FindElement(By.Id("btn_save")).Click();

                Thread.Sleep(3000);
                Assert.IsTrue(_browser.FindElement(By.XPath("//td[@data-title='Firma Adi']")).Enabled);

                Thread.Sleep(5000);
                _browser.Close();
            }

            [Test]
            public void step_09_insert_ilgilikisitest()
            {
                Random random = new Random();
                var number = random.Next(100000, 999999999);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/custom/new/ilgilikisitest");

                //Text -> Soyad açıklama
                var fieldTypeText = _browser.FindElements(By.XPath("//div[@data-fieldtype='Text']"));

                foreach (var item in fieldTypeText)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("test");
                }

                //Lookup -> Bölüm
                EnsureFindElement("//span[@id='select2-chosen-5']");
                EnsureFindElement("select2-chosen-5");

                //SelectList -> Ünvan
                EnsureFindElement("select2-chosen-2");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");
                
                //SelectList -> Cinsiyet
                EnsureFindElement("//span[@id='select2-chosen-3)']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Müşteri Temsilcisi
                EnsureFindElement("select2-chosen-4");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Firma
                EnsureFindElement("//span[@id='select2-chosen-1']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //Phone -> GSM
                var fieldTypePhone = _browser.FindElements(By.XPath("//div[@data-fieldtype='Phone']"));

                foreach (var item in fieldTypePhone)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("121212121212");
                }

                //Email
                var fieldTypeEmail = _browser.FindElements(By.XPath("//div[@data-fieldtype='Email']"));

                foreach (var item in fieldTypeEmail)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("sefa@argeset.com");
                }

                //Number -> TC Kimlik no
                var fieldTypeNumber = _browser.FindElements(By.XPath("//div[@data-fieldtype='Number']"));

                foreach (var item in fieldTypeNumber)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("" + number);
                }

                //Save and close browser
                _browser.FindElement(By.Id("btn_save")).Click();

                Thread.Sleep(3000);
                Assert.IsTrue(_browser.FindElement(By.XPath("//td[@data-title='Ad']")).Enabled);

                Thread.Sleep(5000);
                _browser.Close();
            }

            [Test]
            public void step_10_insert_aktivitetest()
            {
                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/custom/new/aktivitetest");

                //Text 
                var dataFieldTypeText = _browser.FindElements(By.XPath("//div[@data-fieldtype='Text']"));

                foreach (var item in dataFieldTypeText)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("Text");
                }

                //Lookup -> İlgili Firma
                EnsureFindElement("//span[@id='select2-chosen-1']");
                EnsureFindElement("//span[@id='select2-chosen-1']");

                //Lookup -> ilgili Kişi
                EnsureFindElement("//span[@id='select2-chosen-2']");
                EnsureFindElement("//span[@id='select2-chosen-2']");

                //SelectList -> Aktivite Türü
                EnsureFindElement("//span[@id='select2-chosen-3']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Aktivite Şekli
                EnsureFindElement("//span[@id='select2-chosen-4']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Öncelik Durumu
                EnsureFindElement("//span[@id='select2-chosen-5']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //DateTime -> Aktivite Tarihi
                var dataFieldTypeDatetime = _browser.FindElements(By.XPath("//div[@data-fieldtype='DateTime']"));

                foreach (var item in dataFieldTypeDatetime)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("1");
                    EnsureFindElement("//label[@for='" + id + "']");
                }

                //Save and close browser
                _browser.FindElement(By.Id("btn_save")).Click();

                Thread.Sleep(2000);
                Assert.IsTrue(_browser.FindElement(By.XPath("//td[@data-title='Aktivite Açıklaması']")).Enabled);

                Thread.Sleep(5000);
                _browser.Close();
            }

            [Test]
            public void step_11_insert_gorevTest()
            {
                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/custom/new/gorevtest");

                //Text 
                var dataFieldTypeText = _browser.FindElements(By.XPath("//div[@data-fieldtype='Text']"));

                foreach (var item in dataFieldTypeText)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("Text");
                }

                //SelectList -> Görev Türü
                EnsureFindElement("//span[@id='select2-chosen-3']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Öncelik
                EnsureFindElement("//span[@id='select2-chosen-4']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Görev Durumu
                EnsureFindElement("//span[@id='select2-chosen-5']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //Lookup -> İlgili Firma
                EnsureFindElement("//span[@id='select2-chosen-1']");
                Thread.Sleep(1000);
                EnsureFindElement("//span[@id='select2-chosen-1']");

                //Lookup -> ilgili Kişi
                EnsureFindElement("//span[@id='select2-chosen-2']");
                Thread.Sleep(1000);
                EnsureFindElement("//span[@id='select2-chosen-2']");

                //SystemLookUp -> Atanan
                EnsureFindElement("//span[@id='select2-chosen-6']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //DateTime Görev Başlangıç - Bitiş Tarihi
                var dataFieldTypeDatetime = _browser.FindElements(By.XPath("//div[@data-fieldtype='DateTime']"));

                foreach (var item in dataFieldTypeDatetime)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("1");
                    Thread.Sleep(3000);
                    _browser.FindElement(By.XPath("//label[@for='" + id + "']")).Click();
                }

                //Save and close browser
                _browser.FindElement(By.Id("btn_save")).Click();

                Thread.Sleep(3000);
                Assert.IsTrue(_browser.FindElement(By.XPath("//td[@data-title='Görev Açıklaması']")).Enabled);

                Thread.Sleep(5000);
                _browser.Close();
            }

            [Test]
            public void step_12_insert_urunTest()
            {
                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/custom/new/uruntest");

                //Text 
                var dataFieldTypeText = _browser.FindElements(By.XPath("//div[@data-fieldtype='Text']"));

                foreach (var item in dataFieldTypeText)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("Text");
                }

                //SelectList -> Tedarik Firması
                EnsureFindElement("//span[@id='select2-chosen-4']");
                Thread.Sleep(1000);
                EnsureFindElement("//span[@id='select2-chosen-4']");

                //SelectList -> Ürün Birim Seti
                EnsureFindElement("//span[@id='select2-chosen-2']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Ürün Fiyat Para Birimi
                EnsureFindElement("//span[@id='select2-chosen-3']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //SelectList -> Stok Takip Şekli
                EnsureFindElement("//span[@id='select2-chosen-4']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //Number 
                var fieldTypeNumber = _browser.FindElements(By.XPath("//div[@data-fieldtype='Number']"));

                foreach (var item in fieldTypeNumber)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("1");
                }

                //Percent 
                var dataFieldTypePercent = _browser.FindElements(By.XPath("//div[@data-fieldtype='Percent']"));

                foreach (var item in dataFieldTypePercent)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("321");
                }

                //Lookup -> ilgili Kişi
                _browser.FindElement((By.Id("select2-chosen-4"))).Click();
                Thread.Sleep(3000);
                _browser.FindElement((By.Id("select2-chosen-4"))).Click();

                //save and close browser
                _browser.FindElement(By.Id("btn_save")).Click();

                Thread.Sleep(3000);
                Assert.IsTrue(_browser.FindElement(By.XPath("//td[@data-title='Ürün Kodu']")).Enabled);

                Thread.Sleep(5000);
                _browser.Close();
            }

        }

        [TestFixture]
        public class DialyTests
        {
            public FirefoxDriver _browser;
            public int day = (int)System.DateTime.Now.Day;
            public int month = (int)System.DateTime.Now.Month;
            public int hour = (int)System.DateTime.Now.Hour;
            public string[] userName = new string[] { "Name1", "Name2", "Name3", "Name4" };
            public string[] userSurname = new string[] { "SName1", "SName2", "SName3", "SName4" };
            public string[] email = new string[] { "Mail1", "Mail2", "Mail3", "Mail4" };

            [SetUp]
            public void Setup()
            {
                _browser = new FirefoxDriver();
            }

            public void SignIn()
            {
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/user/login");

                _browser.FindElementById("Email").SendKeys(month + "-" + day + "@argeset.com");
                _browser.FindElementById("Password").SendKeys("password");

                _browser.FindElementById("frm").Submit();
            }

            private void EnsureFindElement(string key)
            {
                var isNotVisible = true;
                while (isNotVisible)
                {
                    try
                    {
                        _browser.FindElement(
                            By.XPath(key
                                ))
                            .Click();

                        isNotVisible = false;
                    }
                    catch (Exception)
                    {
                        isNotVisible = true;
                    }
                }
            }

            [Test]
            public void step_01_define_new_user()
            {
                SignIn();

                for (int i = 0; i < userName.Length - 1; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/company/newuser");

                    _browser.FindElement(By.Id("FirstName")).SendKeys(userName[i]);
                    _browser.FindElementById("LastName").SendKeys(userSurname[i]);
                    _browser.FindElementById("Email").SendKeys(email[i] + "@argeset.com");
                    _browser.FindElementById("Password").SendKeys("password");

                    _browser.FindElementById("btn_save_and_new").Click();
                }

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/company/newuser");

                _browser.FindElementById("FirstName").SendKeys(userName[3]);
                _browser.FindElementById("LastName").SendKeys(userSurname[3]);
                _browser.FindElementById("Email").SendKeys(email[3] + "@argeset.com");
                _browser.FindElementById("Password").SendKeys("password");

                _browser.FindElementById("btn_save").Click();

                _browser.Close();
            }

            [Test]
            public void step_02_define_new_user_group()
            {
                SignIn();

                for (int i = 0; i < userName.Length - 1; i++)
                {
                    _browser.Navigate().GoToUrl("http://dev.setcrm.com/usergroup/new");

                    //EnsureFindElement("//a[@class='select2-choice select2-default']");
                    //EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");
                    _browser.FindElement(By.Id("Name")).SendKeys("Group " + userName[i]);
                    _browser.FindElement(By.Id("Description")).SendKeys("Add Description");
                    _browser.FindElement(By.Id("IsActive")).Click();

                    _browser.FindElementById("btn_save_and_new").Click();

                }

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/usergroup/new");

                _browser.FindElement(By.Id("Name")).SendKeys("Group " + userName[3]);
                _browser.FindElement(By.Id("Description")).SendKeys("Add Description");
                _browser.FindElement(By.Id("IsActive")).Click();

                _browser.FindElementById("btn_save").Click();

                _browser.Close();
            }

            [Test]
            public void step_03_define_costum_object()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customobject/list");

                EnsureFindElement("//a[@class='btn btn-sm btn-success']");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");
                _browser.FindElement(By.Id("Name")).SendKeys("A Firma");
                _browser.FindElement(By.Id("RequiredFieldName")).SendKeys("Required Field");

                _browser.FindElement(By.Id("IsActive")).Click();

                _browser.FindElement(By.Id("btn_save")).Click(); //btn_save_and_new

                _browser.Close();
            }

            [Test]
            public void step_04_costum_field_wizard_add_text()
            {
                Random random = new Random();
                int number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='1']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Text " + number);
                //_browser.FindElementById("Description").SendKeys("Add Description");
                //_browser.FindElementById("HelpText").SendKeys("HelpText");
                //_browser.FindElement(By.Id("DefaultValue")).SendKeys("Default Value");
                //_browser.FindElementById("IsRequired").Click();
                //_browser.FindElementById("IsUnique").Click();

                _browser.FindElementById("Length").SendKeys("8");

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_05_costum_field_wizard_add_textarea()
            {
                Random random = new Random();
                int number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='2']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("TextArea " + number); //name should be unique
                //_browser.FindElementById("Description").SendKeys("Add Description");
                //_browser.FindElementById("HelpText").SendKeys("HelpText");
                //_browser.FindElement(By.Id("DefaultValue")).SendKeys("Default Value");
                //_browser.FindElementById("IsRequired").Click();

                _browser.FindElementById("Length").SendKeys("" + number);//try to send random value          

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_06_costum_field_wizard_add_number()
            {
                Random random = new Random();
                int number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='3']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Number " + number);

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_07_costum_field_wizard_add_checkbox()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='4']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("CheckBox " + number);
                //_browser.FindElementById("Description").SendKeys("Add Description");
                //_browser.FindElementById("HelpText").SendKeys("Help Text");

                //_browser.FindElement(By.XPath("//input[@value='true']")).Click();   Radio Button

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_08_costum_field_wizard_add_email()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='5']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("EMail " + number);

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_09_costum_field_wizard_add_phone()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='6']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Phone " + number);

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_10_costum_field_wizard_add_url()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='7']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Url " + number);

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_11_costum_field_wizard_add_percent()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='8']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Percent " + number);

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_12_costum_field_wizard_add_datetime()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']"); ;
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='9']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("DateTime " + number);

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_13_costum_field_wizard_add_date()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='9']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Date " + number);

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_14_costum_field_wizard_add_time()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='11']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("Time " + number);

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_15_costum_field_wizard_add_selectlist()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='12']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementById("Name").SendKeys("SelectList " + number);
                _browser.FindElementById("SelectListItems").SendKeys("Select List Item");

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElementByClassName("wizard-finish").Click();

                _browser.Close();
            }

            [Test]
            public void step_16_costum_field_wizard_add_lookup()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                //Lookup
                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='13']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                EnsureFindElement("//a[@class='select2-choice select2-default']");

                Thread.Sleep(2000);
                var getTextid = _browser.FindElements(By.XPath("//input[@class='select2-input select2-focused']"));

                foreach (var item in getTextid)
                {
                    var id = item.GetAttribute("id");
                    _browser.FindElement(By.Id(id)).SendKeys("A Firma");
                }
                Thread.Sleep(2000);
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.Id("Name")).SendKeys("Lookup " + number);

                _browser.FindElement(By.XPath("//a[@class='wizard-next']")).Click();
                Thread.Sleep(3000);

                _browser.FindElementByClassName("wizard-finish").Click();
            }

            [Test]
            public void step_17_costum_field_wizard_add_system_lookup()
            {
                Random random = new Random();
                var number = random.Next(1, 10);

                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/customfield/wizard");

                EnsureFindElement("//span[@class='select2-arrow']");
                _browser.FindElementById("s2id_autogen1_search").SendKeys("A Firma");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                _browser.FindElement(By.XPath("//input[@value='14']")).Click();

                _browser.FindElementByClassName("wizard-next").Click();
                Thread.Sleep(2000);

                _browser.FindElement(By.Id("Name")).SendKeys("System LookUp Name " + number);

                _browser.FindElement(By.XPath("//a[@class='wizard-next']")).Click();

                _browser.FindElement(By.XPath("//a[@class='wizard-finish']")).Click();

                _browser.Close();
            }

            [Test]
            public void step_18_insert_costum_object()
            {
                SignIn();

                _browser.Navigate().GoToUrl("http://dev.setcrm.com/custom/new/a-firma");

                //Text
                var fieldTypeText = _browser.FindElements(By.XPath("//div[@data-fieldtype='Text']"));

                foreach (var item in fieldTypeText)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("Text");

                }

                //CheckBox
                //var fieldTypeCheckbox = _browser.FindElements(By.XPath("//div[@data-fieldtype='Checkbox']"));

                //foreach (var item in fieldTypeCheckbox)
                //{
                //    var id = item.GetAttribute("data-id");
                //    _browser.FindElement(By.Id(id)).Click();
                //}

                //TextArea
                var fieldTypeTextArea = _browser.FindElements(By.XPath("//div[@data-fieldtype='TextArea']"));

                foreach (var item in fieldTypeTextArea)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("TextAreaField");
                }

                //Number
                var fieldTypeNumber = _browser.FindElements(By.XPath("//div[@data-fieldtype='Number']"));

                foreach (var item in fieldTypeNumber)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("121212121212");
                }

                //Email
                var fieldTypeEmail = _browser.FindElements(By.XPath("//div[@data-fieldtype='Email']"));

                foreach (var item in fieldTypeEmail)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("deneme@argeset.com");
                }

                //Phone
                var fieldTypePhone = _browser.FindElements(By.XPath("//div[@data-fieldtype='Phone']"));

                foreach (var item in fieldTypePhone)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("00902124343434");
                }

                //Url
                var fieldTypeUrl = _browser.FindElements(By.XPath("//div[@data-fieldtype='Url']"));

                foreach (var item in fieldTypeUrl)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("http://www.argeset.com");
                }

                //Percent
                var fieldTypePercent = _browser.FindElements(By.XPath("//div[@data-fieldtype='Percent']"));

                foreach (var item in fieldTypePercent)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("123");
                }

                //DateTime
                var fieldTypeDateTime = _browser.FindElements(By.XPath("//div[@data-fieldtype='DateTime']"));

                foreach (var item in fieldTypeDateTime)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("321");
                    _browser.FindElement(By.XPath("//label[@for='" + id + "']")).Click();
                }


                //Time
                var fieldTypeTime = _browser.FindElements(By.XPath("//div[@data-fieldtype='Time']"));

                foreach (var item in fieldTypeTime)
                {
                    var id = item.GetAttribute("data-id");
                    _browser.FindElement(By.Id(id)).SendKeys("1234");
                    Thread.Sleep(2000);
                    _browser.FindElement(By.XPath("//label[@for='" + id + "']")).Click();
                }

                //SelectList
                EnsureFindElement("//span[@id='select2-chosen-1']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //LookUp
                EnsureFindElement("//span[@id='select2-chosen-2']");
                EnsureFindElement("//span[@id='select2-chosen-2']");

                //SystemLookUp
                EnsureFindElement("//span[@id='select2-chosen-3']");
                EnsureFindElement("//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']");

                //Save
                _browser.FindElement(By.Id("btn_save")).Click();
            }
        }
    }
}
