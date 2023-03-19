using Data.Context;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Fidele.Models.Categories;

namespace Test_Fidele.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        public DataContext dataContext;

        public CategorieController(DataContext dataContext) { 
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var CategoriesQueriable = dataContext.Set<Categorie>();
            var categories = await CategoriesQueriable.Include(x => x.Produits)
                .OrderBy(x => x.Code)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return Ok(new CategorieRepList { 
                totale = CategoriesQueriable.Count(), 
                Categories= categories.Select(x => new CategorieRep 
                { 
                    Code = x.Code, 
                    Libelle = x.Libelle
                })
                .ToList()
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id = 1)
        {
            try
            {
                var CategoriesQueriable = dataContext.Set<Categorie>();
                var categorie = await CategoriesQueriable.SingleOrDefaultAsync(x => x.Code == id);
                return Ok(new CategorieRep { 
                    Code= categorie.Code, 
                    Libelle= categorie.Libelle
                });
            }
            catch(Exception ex) 
            { 
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Categorie categorie)
        {
            try
            {
                var categorieToAdd = await this.dataContext.Set<Categorie>().AddAsync(categorie);
                await this.dataContext.SaveChangesAsync();
                return Created($"https://localhost:7185/api/categorie/{categorie.Code}", new CategorieRep { 
                    Code = categorie.Code, 
                    Libelle = categorie.Libelle 
                });
            }
            catch(Exception ex)
            {
                return Problem("Declenche un probleme :" + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categorie categorie)
        {
            try
            {
                if(categorie.Code != id)
                {
                    return BadRequest();
                }

                var categorieToAdd = await this.dataContext.Set<Categorie>()
                                                 .FirstOrDefaultAsync(x => x.Code == categorie.Code);
                if (categorieToAdd == null)
                {
                    return NotFound();
                }

                categorieToAdd.Libelle = categorie.Libelle;
                await this.dataContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem("Declenche un probleme :" + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var categorieToDel = await this.dataContext.Set<Categorie>().SingleAsync(x => x.Code == id);
                this.dataContext.Remove(categorieToDel);
                await this.dataContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem("Declenche un probleme :" + ex.Message);
            }
        }
    }
}
