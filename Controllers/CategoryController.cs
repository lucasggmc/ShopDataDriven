using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    [Route("")]
    public string Get()
    {
        return "get";
    }

    [HttpGet]
    [Route("{id}")]
    public string GetById(int id)
    {
        return "get by id";
    }

    [HttpPost]
    [Route("")]
    public string Post()
    {
        return "post";
    }

    [HttpPut]
    [Route("")]
    public string Put()
    {
        return "put";
    }

    [HttpDelete]
    [Route("")]
    public string Delete()
    {
        return "delete";
    }
}