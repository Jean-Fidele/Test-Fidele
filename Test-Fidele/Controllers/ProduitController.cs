using Data.Context;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Fidele.Models.Categories;
using Test_Fidele.Models.Produits;

namespace Test_Fidele.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        public DataContext dataContext;
        private readonly ILogger<ProduitController> _logger;

        public ProduitController(DataContext dataContext, ILogger<ProduitController> logger)
        {
            this.dataContext = dataContext;
            _logger = logger;
        }

        /*
         * Get-all
         * api/produit
         */
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int categorieId = 0, 
                                                [FromQuery] int page = 1, 
                                                [FromQuery] int size = 5)
        {
            //Check users data
            try
            {
                var totale = await this.dataContext.Set<Produit>().CountAsync();
                var produitsQuery = this.dataContext.Set<Produit>()
                                             .Include(x => x.Categorie)
                                             .AsQueryable<Produit>();

                if (categorieId > 0)
                {
                    produitsQuery = produitsQuery.Where(x => x.CategorieId == categorieId);
                }
                var produits = await produitsQuery.OrderBy(x => x.Code)
                                            .Skip((page - 1) * size)
                                            .Take(size).ToListAsync();

                var resultats = new ProduitRepList
                {
                    totale = totale,
                    Produits = produits.Select(x => new ProduitRep
                    {
                        Code = x.Code,
                        Name = x.Name,
                        Categorie = new CategorieRep
                        {
                            Code = x.Categorie.Code,
                            Libelle = x.Categorie.Libelle
                        }
                    }).ToList()
                };

                return Ok(resultats);
            }
            catch (Exception ex)
            {
                return Problem("Declenche un probleme :" + ex.Message);
            }
        }

        /*
         * Get-ById
         * api/produit
         */
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try 
            { 
                var produit = await this.dataContext.Set<Domain.Produit>()
                                           .Include(x => x.Categorie)
                                           .SingleAsync(x => x.Code == id);
                return Ok(new ProduitRep { 
                    Code = produit.Code, 
                    Name = produit.Name, 
                    Categorie = new Models.Categories.CategorieRep { 
                        Code = produit.Categorie.Code, 
                        Libelle = produit.Categorie.Libelle
                    } 
                });
            }
            catch(Exception ex)
            {
                return Problem($"Error : {ex.Message}");
            }
        }

        /*
         * Post
         * api/produit
         */
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produit produit)
        {
            try
            {
                var prodAdd = await this.dataContext.Set<Produit>().AddAsync(produit);
                Produit prod = prodAdd.Entity;
                await this.dataContext.SaveChangesAsync();
                return Created("https://localhost:7185/api/produit/" + produit.Code, new ProduitRep
                {
                    Code = produit.Code,
                    Name = produit.Name,
                    //Categorie = new Models.Categories.CategorieRep
                    //{
                    //    Code = produit.Categorie.Code,
                    //    Libelle = produit.Categorie.Libelle
                    //}
                });
            }
            catch (Exception ex) 
            {
                return Problem($"Error : {ex.Message}");
            }
        }

        /*
         * Put
         * api/produit
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Produit produit)
        {
            try
            {
                if(produit.Code != id)
                {
                    return BadRequest(produit);
                }

                var produitToUpdate = await this.dataContext.Set<Domain.Produit>()
                                                     .SingleOrDefaultAsync(x => x.Code == id);
                if(produitToUpdate == null)
                {
                    return NotFound();
                }

                produitToUpdate.Code = produit.Code;
                produitToUpdate.Name = produit.Name;

                await this.dataContext.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex) 
            {
                return Problem($"Error : {ex.Message}");
            }
        }
            
        /*
        * Delete
        * api/produit/5
        */
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var produitToRemove = await this.dataContext.Set<Domain.Produit>().SingleOrDefaultAsync(x => x.Code == id);
                if(produitToRemove == null)
                {
                    return NotFound();
                }
                this.dataContext.Remove(produitToRemove);
                await this.dataContext.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                return Problem($"Error : {ex.Message}");
            }
        }
    }
}
