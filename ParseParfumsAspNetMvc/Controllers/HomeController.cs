using HtmlAgilityPack;
using PagedList;
using ParseParfumsAspNetMvc.Interfaces;
using ParseParfumsAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ParseParfumsAspNetMvc.Controllers
{
    public class HomeController : Controller
    {
        private string url = "https://parfums.ua/category/lady";
        
        private IProductRepository pr;

        public HomeController(IProductRepository pr)
        {
            this.pr = pr;
        }

        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var products = pr.GetAll().ToPagedList(pageNumber, pageSize);

            return View(products);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = pr.Get(id);
            var prices = pr.GetAllPrices(id).ToList();
            ViewBag.Prices = prices;

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult Parse()
        {
            var products = pr.GetAll().ToList();
            var parseProducts = GetParseList(url);

            // Если в БД нет данных, добавляет все, что спарсили
            if(products.Count == 0)
            {
                pr.AddRange(parseProducts);
                pr.Save();

                products = pr.GetAll().ToList();

                foreach(var item in products)
                {
                    item.Prices.Add(new PriceProduct
                    {
                        PriceOrigin = item.PriceOrigin,
                        Price = item.Price,
                        Date = DateTime.Now
                    });
                }

                pr.Save();

                return RedirectToAction("Index");
            }

            // Проходим по всему списку спарсеных товаров и сверяемся с БД
            for (int i = 0; i < parseProducts.Count; i++)
            {
                bool flag = true;
                for (int j = 0; j < products.Count; j++)
                {
                    // Если уже есть такой товар в БД
                    if(parseProducts[i].Name.Equals(products[j].Name))
                    {
                        flag = false;

                        // Если цена не изменилась, не трогаем этот товар
                        // Иначе добавляем новую цену для динамики цен
                        if(parseProducts[i].Price == products[j].Price)
                        {
                            break;
                        }
                        else
                        {
                            products[j].Prices.Add(new PriceProduct
                            {
                                PriceOrigin = products[j].PriceOrigin,
                                Price = products[j].Price,
                                Date = DateTime.Now
                            });
                            pr.Save();

                            break;
                        }
                    }
                }

                // Если Товара не было в БД, добавляем его
                if(flag)
                {
                    parseProducts[i].Prices.Add(new PriceProduct
                    {
                        PriceOrigin = parseProducts[i].PriceOrigin,
                        Price = parseProducts[i].Price,
                        Date = DateTime.Now
                    });
                    pr.Add(parseProducts[i]);
                    pr.Save();
                }
            }

            return RedirectToAction("Index");
        }

        // Парсим с HtmlAgilityPack
        private List<Product> GetParseList(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            List<Product> products = new List<Product>();

            var nodes = doc.DocumentNode.SelectNodes("//*[@id=\"wrapper\"]/section/div[3]/div/div[1]/div[2]/div/div");

            if(nodes == null)
            {
                return products;
            }

            foreach(var item in nodes)
            {
                string name = item.SelectSingleNode("div[3]/div[1]/a/span").InnerText;
                string priceOrigin = item.SelectSingleNode("div[3]/div[2]/div[2]/span[1]").InnerText;
                decimal price = Decimal.Parse(priceOrigin.Remove(priceOrigin.Length - 3).Replace(" ", String.Empty));
                string imageSrc = String.Concat("https://parfums.ua", item.SelectSingleNode("div[2]/a/img").GetAttributeValue("src", ""));

                products.Add(new Product
                {
                    Name = name,
                    PriceOrigin = priceOrigin,
                    Price = price,
                    ImageSrc = imageSrc
                });
            }

            return products;
        }

        protected override void Dispose(bool disposing)
        {
            pr.Dispose();
            base.Dispose(disposing);
        }
    }
}