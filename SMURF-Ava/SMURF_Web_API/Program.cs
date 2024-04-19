using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SMURF_Web_API;

public class Program {
    public static void Main(string[] args) {

        int portNum = args.Length == 0? 3506 : int.Parse(args[0]);
        SMURF_TCP_Client client = new SMURF_TCP_Client("127.0.0.1", portNum);
        
        var builder = WebApplication.CreateBuilder();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();


        app.MapPost("/statusReturn", (StatusContent statusContent) => {

                client.Send(statusContent.Content);
                return Results.Ok("StatusReturn Received! " + statusContent.Content);

            })
            .WithName("PostStatusReturn");

        app.Run();
    }
}





