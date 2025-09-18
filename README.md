## Guide to run this template

## Requirements:
- Mysql 8.0
- Redis 
- Cloudinary
- Mail
- RabbitMQ
- Docker (dunno if u want to run this on docker)

### Configuration (appsetting.json) ###
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "REMOTE_MYSQL_URL": ""
  },
  "Jwt": {
    "SecretKey": "",
    "Issuer": "",
    "Audience": "",
    "atExpiryInMillisecond": "1*60*1000"
  },
  "Cloudinary": {
    "url": ""
  },
  "Redis": {
    "url": "localhost",
    "port": "6379",
    "password": "",
    "user": ""
  },
  "Smtp": {
    "host": "smtp.gmail.com",
    "port": "587",
    "user": "",
    "pass": "",
    "from": ""
  },
  "RabbitMq": {
    "host": "localhost",
    "user": "guest",
    "pass": "guest"
  }
}

```

