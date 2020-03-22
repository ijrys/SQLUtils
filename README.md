# SQLUtils

`MiRaI SQLUtils` is a lite and quick tools of Ado.Net
**It's not a ORM Framework**

## SQL

use `SQL.DefaultConnectionString` to set default connection string

use `SQL.Connection()` to get a Connection by  `DefaultConnectionString`

## select

``` c#
Dictionary<string, object> values = SQL.Connection()
        .Select()
        .From("MainTable", "m")
        .Outer.Join(
            new SelectCommand()
            .Select()
            .From("ExtraTable")
            .Where("[ID] < 5"), "e")
        .On("m.Id = e.ID")
        .Where("a.[Id] = @id")
        .AddParameter("id", 1)
        .Execute()
        .FirstLine((SqlDataReader reader) => {
            Dictionary<string, object> values = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++) {
                values[reader.GetName(i)] = reader.GetValue(i);
            }
            return values;
        }) as Dictionary<string, object>;

foreach (var item in values) {
    Console.WriteLine($"{item.Key}    \t | {item.Value}");
}
```

## Update

``` c#
int re = SQL.Connection()
    .Update().From("MainTable")
    .AppendSet("Name", "sqlutils")
    .Where("[id] = @id")
    .AddParameter("id", 1)
    .Execute()
    .NonQuery();
Console.WriteLine($"{re} row(s) changed");
```

