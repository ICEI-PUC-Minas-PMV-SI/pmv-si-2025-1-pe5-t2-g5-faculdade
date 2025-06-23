using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

public class TesteModel : PageModel
{
    public void OnPost()
    {
        Console.WriteLine("✅ POST acionado!");
    }
}