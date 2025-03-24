using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCC___Obra.Data;
using TCC___Obra.Models;

namespace TCC___Obra.Controllers
{
    public class ObrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Obras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Obra.ToListAsync());
        }

        // GET: Obras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra
                .FirstOrDefaultAsync(m => m.ObraId == id);
            if (obra == null)
            {
                return NotFound();
            }

            return View(obra);
        }

        // GET: Obras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Obras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Obra obra, IFormFile FotoArquivo)
        {
           

            // Validação de CPF ou CNPJ com base no tipo de pessoa
            if (obra.TipoPessoa == "Fisica" && string.IsNullOrEmpty(obra.Cpf))
            {
                ModelState.AddModelError("Cpf", "O CPF é obrigatório para Pessoa Física.");
            }
            if (obra.TipoPessoa == "Juridica" && string.IsNullOrEmpty(obra.Cnpj))
            {
                ModelState.AddModelError("Cnpj", "O CNPJ é obrigatório para Razão Social.");
            }

            ModelState.Remove("FotoObra");

            if (ModelState.IsValid)
            {
                if (FotoArquivo != null && FotoArquivo.Length > 0)
                {
                    var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");
                    Directory.CreateDirectory(pastaDestino); // Garante que a pasta existe

                    var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(FotoArquivo.FileName);
                    var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        await FotoArquivo.CopyToAsync(stream);
                    }

                    obra.FotoObra = "/imagens/" + nomeArquivo;
                }

                _context.Add(obra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Se houver erros de validação, retorna à view de criação com as mensagens de erro
            return View(obra);
        }

        // GET: Obras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }
            return View(obra);
        }

        // POST: Obras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Obra obra, IFormFile FotoArquivo)
        {
            if (id != obra.ObraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var obraOriginal = await _context.Obra.FindAsync(id);
                    if (obraOriginal == null)
                    {
                        return NotFound();
                    }

                    // Atualiza os dados
                    obraOriginal.NomeObra = obra.NomeObra;
                    obraOriginal.NomeCliente = obra.NomeCliente;
                    obraOriginal.TipoPessoa = obra.TipoPessoa;
                    obraOriginal.Cpf = obra.Cpf;
                    obraOriginal.Cnpj = obra.Cnpj;
                    obraOriginal.Email = obra.Email;
                    obraOriginal.Telefone = obra.Telefone;
                    obraOriginal.Localizacao = obra.Localizacao;
                    obraOriginal.StatusObra = obra.StatusObra;
                    obraOriginal.DtInicio = obra.DtInicio;
                    obraOriginal.DtEstimada = obra.DtEstimada;
                    obraOriginal.RespObra = obra.RespObra;

                    // Se tiver nova imagem, substitui
                    if (FotoArquivo != null && FotoArquivo.Length > 0)
                    {
                        var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");
                        Directory.CreateDirectory(pastaDestino);

                        var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(FotoArquivo.FileName);
                        var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            await FotoArquivo.CopyToAsync(stream);
                        }

                        // Atualiza o caminho da imagem
                        obraOriginal.FotoObra = "/imagens/" + nomeArquivo;
                    }

                    _context.Update(obraOriginal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObraExists(obra.ObraId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(obra);
        }
        // GET: Obras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obra
                .FirstOrDefaultAsync(m => m.ObraId == id);
            if (obra == null)
            {
                return NotFound();
            }
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Erro em {modelState.Key}: {error.ErrorMessage}");
                }
            }

            return View(obra);
        }

        // POST: Obras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obra = await _context.Obra.FindAsync(id);
            if (obra != null)
            {
                _context.Obra.Remove(obra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObraExists(int id)
        {
            return _context.Obra.Any(e => e.ObraId == id);
        }
    }
}
