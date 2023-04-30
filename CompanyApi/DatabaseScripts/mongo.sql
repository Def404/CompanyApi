use hardDriveShope

db.createCollection("hardDrive",{
    validator: {
        $jsonSchema: {
            bsonType: "object",
            title: "Hard Drive Object Validation",
            required: ["ps_id", "name", "size", "count", "price", "category_id", "connection_type_id"],
            properties: {
                "ps_id": {
                    bsonType: "int"
                },
                "name": {
                    bsonType: "string",
                    maximum: 100
                },
                "size": {
                    bsonType: "int"
                },
                "count": {
                    bsonType: "int"
                },
                "price": {
                    bsonType: "int"
                },
                "category_id":{
                    bsonType: "objectId"
                },
                "connection_type_id":{
                    bsonType: "objectId"
                }
            }
        }
    }
});


db.createCollection("driveType",{
    validator: {
        $jsonSchema: {
            bsonType: "object",
            title: "Hard Drive Type Object Validation",
            required: ["ps_id", "name"],
            properties: {
                "ps_id": {
                    bsonType: "int"
                },
                "name": {
                    bsonType: "string",
                    maximum: 10
                }
            }
        }
    }
});


db.createCollection("connectionInterfaceType",{
    validator: {
        $jsonSchema: {
            bsonType: "object",
            title: "Connection Interface Type Object Validation",
            required: ["ps_id", "name", "transfer_rate"],
            properties: {
                "ps_id": {
                    bsonType: "int"
                },
                "name": {
                    bsonType: "string",
                    maximum: 20
                },
                "transfer_rate": {
                    bsonType: "int"
                }
            }
        }
    }
});


db.createCollection("employee",{
    validator: {
        $jsonSchema: {
            bsonType: "object",
            title: "Employee Object Validation",
            required: ["login", "full_name", "email", "phone_number", "position"],
            properties: {
                "login": {
                    bsonType: "string"
                },
                "full_name": {
                    bsonType: "string",
                    maximum: 100
                },
                "email": {
                    bsonType: "string",
                    maximum: 320
                },
                "phone_number": {
                    bsonType: "string",
                    maximum: 12
                },
                "position": {
                    bsonType: "string",
                    maximum: 25
                }
            }
        }
    }
});


db.createCollection("client",{
    validator: {
        $jsonSchema: {
            bsonType: "object",
            title: "Client Object Validation",
            required: [ "full_name", "email", "phone_number", "position"],
            properties: {
                "full_name": {
                    bsonType: "string",
                    maximum: 100
                },
                "email": {
                    bsonType: "string",
                    maximum: 320
                },
                "phone_number": {
                    bsonType: "string",
                    maximum: 12
                },
                "date_of_birth": {
                    bsonType: "date"
                }
            }
        }
    }
});

db.createCollection("order",{
    validator: {
        $jsonSchema: {
            bsonType: "object",
            title: "Order Object Validation",
            required: ["client_id", "employee_id", "order_date", "status"],
            properties: {
                "client_id": {
                    bsonType: "objectId"
                },
                "employee_id": {
                    bsonType: "objectId"
                },
                "order_date": {
                    bsonType: "date"
                },
                "status": {
                    bsonType: "string"
                },
                "cart": {
                    bsonType: "array",
                    items: {
                        bsonType: "object",
                        required:  ["hard_drive_id", "count"],
                        properties: {
                            "hard_drive_id": {
                                bsonType: "objectId"
                            },
                            "count": {
                                bsonType: "int"
                            }
                        }
                    }
                }
            }
        }
    }
});

//////////////////

db.driveType.insertMany([
    {"ps_id": 1, "name": "HDD"},
    {"ps_id": 2, "name": "SSD"},
]);


db.connectionInterfaceType.insertMany([
    {"ps_id": 1, "name": "ATA", "transfer_rate": 66},
    {"ps_id": 2, "name": "SATA", "transfer_rate": 150},
    {"ps_id": 3, "name": "FireWire", "transfer_rate": 160},
    {"ps_id": 4, "name": "SCSI", "transfer_rate": 320},
    {"ps_id": 5, "name": "SAS", "transfer_rate": 600},
    {"ps_id": 6, "name": "USB", "transfer_rate": 380},
]);

db.hardDrive.insertMany([
    {"ps_id": 1, "name": "SSD BLACK 64", "size": 64, "count": 55, "price": 15000.00, "category_id": ObjectId("644ec6297f8d765f060d90eb"), "connection_type_id": ObjectId("644ec6bb7f8d765f060d90ef")},
    {"ps_id": 2, "name": "HDD White USB 128", "size": 128, "count": 24, "price": 2000.00, "category_id": ObjectId("644ec6297f8d765f060d90ea"), "connection_type_id": ObjectId("644ec6bb7f8d765f060d90f3")},
    {"ps_id": 3, "name": "SSD GOLD 1024", "size": 1024, "count": 45, "price": 25000.00, "category_id": ObjectId("644ec6297f8d765f060d90eb"), "connection_type_id": ObjectId("644ec6bb7f8d765f060d90f2")},
    {"ps_id": 4, "name": "SSD BLACK 32", "size": 32, "count": 51, "price": 7000.00, "category_id": ObjectId("644ec6297f8d765f060d90eb"), "connection_type_id": ObjectId("644ec6bb7f8d765f060d90ef")},
    {"ps_id": 5, "name": "HDD Blue 256", "size": 256, "count": 74, "price": 4000.00, "category_id": ObjectId("644ec6297f8d765f060d90ea"), "connection_type_id": ObjectId("644ec6bb7f8d765f060d90ef")},
]);

db.employee.insertMany([
    {"login": "lazarev", "full_name": "Сергей Вячеславович Лазарев", "email": "lazarev@artzvezdy.ru", "phone_number": "+79857731755", "position": "admin_c"},
    {"login": "ivanova", "full_name": "Иванова Айша Станиславовна", "email": "ivanova@mail.ru", "phone_number": "+79942497578", "position": "worker"},
    {"login": "isaeva", "full_name": "Исаева Мария Никитична", "email": "isaeva@mail.ru", "phone_number": "+79386832616", "position": "manager"},
    {"login": "golubeva", "full_name": "Голубева Софья Тимофеевна", "email": "golubeva@mail.ru", "phone_number": "+79249267433", "position": "manager"},
    {"login": "panin", "full_name": "Панин Артём Матвеевич", "email": "panin@mail.ru", "phone_number": "+79972415610", "position": "manager"},
    {"login": "surkiva", "full_name": "Суркова Василиса Максимовна", "email": "surkiva@mail.ru", "phone_number": "+79515070566", "position": "worker"},
]);
