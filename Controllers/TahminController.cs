using Microsoft.AspNetCore.Mvc;
using YakitTuketimTahmini.Models;
using System.Net.Http.Json;

namespace YakitTuketimTahmini.Controllers
{
    public class TahminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TahminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AracBilgisi model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync("http://localhost:5000/predict", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TahminSonucu>();
                model.TahminiTuketim = result.Prediction;

                var tahminKaydi = new TahminVerisi
                {
                    Tarih = DateTime.Now,
                    Marka = model.Marka,
                    Model = model.Model,
                    Yil = model.Yil,
                    MotorTipi = model.MotorTipi,
                    TahminiTuketim = model.TahminiTuketim,
                    KullaniciIP = HttpContext.Connection.RemoteIpAddress?.ToString()
                };

                _context.Tahminler.Add(tahminKaydi);
                await _context.SaveChangesAsync();
            }

            return View(model);
        }

        // ✅ Tahmin geçmişini görüntüleme metodu
        public IActionResult Gecmis()
        {
            var liste = _context.Tahminler
                .OrderByDescending(t => t.Tarih)
                .ToList();

            return View(liste);
        }

        // ✅ Araç karşılaştırma (GET)
        [HttpGet]
        public IActionResult Karsilastir()
        {
            return View();
        }

        // ✅ Araç karşılaştırma (POST)
        [HttpPost]
        public IActionResult Karsilastir(string marka1, string model1, string marka2, string model2)
        {
            var arac1 = _context.Tahminler
                .Where(t => t.Marka == marka1 && t.Model == model1)
                .OrderByDescending(t => t.Tarih)
                .FirstOrDefault();

            var arac2 = _context.Tahminler
                .Where(t => t.Marka == marka2 && t.Model == model2)
                .OrderByDescending(t => t.Tarih)
                .FirstOrDefault();

            ViewBag.Arac1 = arac1;
            ViewBag.Arac2 = arac2;

            return View();
        }
    }

    // Bu sınıf burada tanımlıysa controller dışına ama namespace içine yazılmalı.
    public class TahminSonucu
    {
        public float Prediction { get; set; }
    }
}
