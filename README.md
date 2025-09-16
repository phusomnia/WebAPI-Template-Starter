* Yeu cau du an:
 - Database 
   * Mysql 8.0 - port 3306 
 - Cache
   * Redis - port 6379
 - Config (appsettings.json)
   * Redis
   * Cloudinary
   * Mysql
   * Email (chi chay local)

```
/
├── .github/ # 
├── Domain / # chua thuc the, core cua nghiep vu
├── Features / # cac chuc nang duoc goi thong API
├── Infrastructure / # truy xuat database, cache 
├── Program.cs # file bat dau chay tu day
└── appsettings.json # file config
```