# SQLUtils

`MiRaI SQLUtils` is a lite and quick tools of Ado.Net

**It's not a ORM Framework**

## SQL

use `SQL.DefaultConnectionString` to set default connection string

use `SQL.Connection()` to get a Connection by  `DefaultConnectionString`

## Select

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

if you haven't set where expression by `[UpdateCommand].Where`,  it will throw `UnSafeExpressionException`

if you want to update all datas, use `[UpdateCommand].SetSafetyCheck(bool)` to close safety check, like below:

``` c#
int re = SQL.Connection()
    .Update().From("User")
    .AppendSet("nickname", "sqlutils")
    .SetSafetyCheck(false)
    .AddParameter("id", 1)
    .Execute()
    .NonQuery();
Console.WriteLine($"{re} row(s) changed");
```

## Insert

``` c#
int re = SQL.Connection()
    .Insert().From("MainTable")
    .Columns("Column0", "Column1", "Column2")
    .Value("Value1", "Value2", DateTime.Now)
    .Execute()
    .NonQuery();
Console.WriteLine($"{re} row(s) changed");
```

## Delete

``` c#
int re = SQL.Connection()
    .Delete().From("MainTable")
    .Where("id = @id")
    .AddParameter("id", 3)
    .Execute()
    .NonQuery();
Console.WriteLine($"{re} row(s) changed");
```

if you haven't set where expression by `[DeleteCommand].Where`,  it will throw `UnSafeExpressionException`

if you want to delete all datas, use `[DeleteCommand].SetSafetyCheck(bool)` to close safety check, like below:

``` c#
int re = SQL.Connection()
    .Delete().From("User")
    .SetSafetyCheck(false)
    .AddParameter("id", 3)
    .Execute()
    .NonQuery();
Console.WriteLine($"{re} row(s) changed");
```

