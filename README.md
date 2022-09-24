# WebApiProject
Practices For Developers 
Swagger Api Enable ASP.Net core

Middleware in C#.Net Core :

The Middleware are C# classes that can handle an HTTP request or response. Middleware can be either: Handle an incoming HTTP request by generating an HTTP response. Process an incoming HTTP request, modify it, and pass it on to another piece of middleware

Learning : https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0

![image](https://user-images.githubusercontent.com/85032095/189492738-a71026a2-0397-4ecd-879e-ba4d33c2c069.png)


Recent Microservice Exception Module Handled based on the with Middleware.

Start up Class :

            //Map() method is used to map the middleware to a specific URL
            app.Map("/ErrorValues", (app) => { });
            app.Map("/Error", MapHandler);
            app.UseMiddleware<ExceptionMiddleware>();

            app.MapWhen(context => context.Request.Query.ContainsKey("Err"), HandleRequestWithQuery);

            //app.Use(async (context, next) => {
            //    Console.WriteLine("Before Request Placed");

            //    await next();

            //    Console.WriteLine("After Request Placed");

            //});

            //app.Run(async context => {
            //    Console.WriteLine("Showing the Middleware.");
            //    await context.Response.WriteAsync("Hello Ranganathan, Welcome to the middleware.");
            //});
  
 Dynamodb Learning .Net Core API 
 
 # https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/CodeSamples.DotNet.html
 # https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio
 
 
[DynamoDB.pdf](https://github.com/ranganathanarul/WebApiProject/files/9639216/DynamoDB.pdf)

Startup Class(Startup.cs)

            //Dynamodb Credemntial Copying
            var credentials = new BasicAWSCredentials("#######", "##############");
            var config = new AmazonDynamoDBConfig()
            {
                RegionEndpoint=Amazon.RegionEndpoint.USEast1,
            };
            var dyClient = new AmazonDynamoDBClient(credentials, config);
            services.AddSingleton<IAmazonDynamoDB>(dyClient);
            services.AddScoped<IDynamoDBContext, DynamoDBContext>();

Annotation Dynamodb :

 [DynamoDBTable("tblEmployee")]
    public class tblEmployee
    {
        [DynamoDBHashKey("empId")]
        public int empId { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("empNumber")]
        public int empNumber { get; set; }

        [DynamoDBProperty("empCity")]
        public string empCity { get; set; }

        [DynamoDBProperty("empFName")]
        public string empFName { get; set; }

        [DynamoDBProperty("empLName")]
        public string empLName { get; set; }

        [DynamoDBProperty("empPhone")]
        public string empPhone { get; set; }

        [DynamoDBProperty("empStatus")]
        public string empStatus { get; set; }

         [DynamoDBProperty("isempActive")]
        public Boolean isempActive { get; set; }

    }

