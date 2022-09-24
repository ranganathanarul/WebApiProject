using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace WebApiSwagger
{

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
}
