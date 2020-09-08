using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

[Route("[controller]")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<Category>>> Get([FromServices]DataContext context)
    {
        var categories = await context.Categories.AsNoTracking().ToListAsync();
        return Ok(categories);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext context)
    {
        var categorie = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return Ok(categorie);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<List<Category>>> Post([FromBody]Category model, [FromServices]DataContext context)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.Categories.Add(model);  
            await context.SaveChangesAsync();
            return Ok(model); 
        }
        catch
        {
            return BadRequest(new {message = "Não foi possível criar a categoria."});            
        }         
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult<List<Category>>> Put(int id, [FromBody]Category model, [FromServices]DataContext context)
    {
        //Verifica se o idd informado é o mesmo do modelo
        if(id != model.Id)
            return NotFound(new {message = "Categoria não encontrada"});

        // verifica se os dados são válidos
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.Entry<Category>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();   
            return Ok(model);
        }
        catch (DbUpdateConcurrencyException)
        {            
            return BadRequest(new {message = "Este registro já foi atualizado."});
        }        
        catch   
        {
            return BadRequest(new {message = "Não foi possível atualizar a categoria."});
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<List<Category>>> Delete(int id, [FromServices]DataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
                return NotFound(new {message = "Categoria não encontrada"});

                try
                {
                    context.Categories.Remove(category);
                    await context.SaveChangesAsync();
                    return Ok(new {message = "Categoria removida com sucesso!"});
                }
                catch
                {                    
                    return BadRequest( new {message = "Não foi possível remover a categoria"});
                }        
    }
}