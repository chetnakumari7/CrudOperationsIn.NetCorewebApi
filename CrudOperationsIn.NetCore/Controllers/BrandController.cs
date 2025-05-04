using Azure.Core;
using CrudOperationsIn.NetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationsIn.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbContext;
        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brands>>> GetBrands()
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();

            }
            return await _dbContext.Brands.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brands>> GetBrandById(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }

        [HttpPost]
        public async Task<ActionResult<Brands>> Postbrands(Brands brand)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Brands.Add(brand);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction("GetBrands", new { id = brand.Id }, brand);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Brands>> UpdateById(int id, Brands updatedBrand)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            brand.Name = updatedBrand.Name;
            brand.Category = updatedBrand.Category;
            brand.IsActive = updatedBrand.IsActive;
            await _dbContext.SaveChangesAsync();
            return brand;

        }

        [HttpDelete]
        public async Task<ActionResult<IEnumerable<Brands>>> DeleteAllBrands()
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var allBrands = await _dbContext.Brands.ToListAsync();
            if (!allBrands.Any())
            {
                return NotFound("no brand found to delete");
            }
            _dbContext.RemoveRange(allBrands);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Brands.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Brands>> DeleteById(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }

            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _dbContext.Brands.Remove(brand);
            await _dbContext.SaveChangesAsync();

            return brand; 
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Brands>> PatchBrand(int id, [FromBody] Brands brandUpdate)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }

            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(brandUpdate.Name))
            {
                brand.Name = brandUpdate.Name;
            }

            if (brandUpdate.IsActive != 0)  
            {
                brand.IsActive = brandUpdate.IsActive;
            }

            await _dbContext.SaveChangesAsync();

            return Ok(brand);  
        }


    }
}
