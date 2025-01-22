# LINQ (Language Integrated Query) - Przewodnik

## Spis treœci
- [Wprowadzenie](#wprowadzenie)
- [Typy Wykonania w LINQ](#typy-wykonania-w-linq)
- [Podstawowe Operacje](#podstawowe-operacje)
- [Operacje Filtrowania](#operacje-filtrowania)
- [Operacje Transformacji](#operacje-transformacji)
- [Operacje Agregacji](#operacje-agregacji)
- [Operacje Sortowania](#operacje-sortowania)
- [Operacje Konwersji](#operacje-konwersji)
- [Najlepsze Praktyki](#najlepsze-praktyki)

## Wprowadzenie

LINQ (Language Integrated Query) to zestaw funkcjonalnoœci w .NET, który wprowadza mo¿liwoœæ wykonywania zapytañ bezpoœrednio w kodzie C#. LINQ upraszcza pracê z kolekcjami danych, bazami danych i XML.

## Typy Wykonania w LINQ

W LINQ wystêpuj¹ dwa typy wykonania zapytañ:

### 1. Immediate Execution (Natychmiastowe Wykonanie)
Operacje wykonywane od razu po wywo³aniu, zwracaj¹ce wynik natychmiast:
- `ToList()`
- `ToArray()`
- `Count()`
- `First()`
- `Last()`
- `Sum()`
- `Average()`
- `Max()`
- `Min()`

### 2. Deferred Execution (Odroczone Wykonanie)
Operacje wykonywane dopiero w momencie faktycznego u¿ycia wyników:
- `Where()`
- `Select()`
- `OrderBy()`
- `GroupBy()`
- `Join()`
- `Skip()`
- `Take()`

## Podstawowe Operacje

### Filtrowanie (Where)
```csharp
var numbers = new[] { 1, 2, 3, 4, 5 };
var evenNumbers = numbers.Where(x => x % 2 == 0);
// Wynik: [2, 4]
```

### Projekcja (Select)
```csharp
var numbers = new[] { 1, 2, 3 };
var doubled = numbers.Select(x => x * 2);
// Wynik: [2, 4, 6]
```

### Take i Skip
```csharp
var numbers = new[] { 1, 2, 3, 4, 5 };
var firstThree = numbers.Take(3);  // [1, 2, 3]
var skipTwo = numbers.Skip(2);     // [3, 4, 5]
```

## Operacje Filtrowania

### OfType
Filtruje elementy okreœlonego typu:
```csharp
object[] mixedArray = { 1, "string", 2, true, 3 };
var onlyNumbers = mixedArray.OfType<int>();
// Wynik: [1, 2, 3]
```

### Where z indeksem
```csharp
var numbers = new[] { 10, 20, 30, 40, 50 };
var elementsWithEvenIndex = numbers.Where((num, index) => index % 2 == 0);
// Wynik: [10, 30, 50]
```

## Operacje Transformacji

### SelectMany
Sp³aszcza zagnie¿d¿one kolekcje:
```csharp
var arrays = new[] { 
    new[] { 1, 2 }, 
    new[] { 3, 4 } 
};
var flattened = arrays.SelectMany(x => x);
// Wynik: [1, 2, 3, 4]
```

### Zip
£¹czy dwie kolekcje:
```csharp
var numbers = new[] { 1, 2, 3 };
var letters = new[] { "A", "B", "C" };
var zipped = numbers.Zip(letters, (n, l) => $"{l}{n}");
// Wynik: ["A1", "B2", "C3"]
```

## Operacje Agregacji

### Aggregate
Redukuje kolekcjê do pojedynczej wartoœci:
```csharp
var numbers = new[] { 1, 2, 3, 4 };

// Podstawowe u¿ycie
var sum = numbers.Aggregate((x, y) => x + y);
// Wynik: 10

// Z wartoœci¹ pocz¹tkow¹
var sumPlusTen = numbers.Aggregate(10, (x, y) => x + y);
// Wynik: 20

// Z transformacj¹ wyniku
var average = numbers.Aggregate(
    0.0,                    // wartoœæ pocz¹tkowa
    (acc, val) => acc + val,// akumulator
    result => result / 4    // transformacja koñcowa
);
// Wynik: 2.5
```

## Operacje Sortowania

### OrderBy i ThenBy
```csharp
var people = new[] {
    new { Name = "Adam", Age = 25 },
    new { Name = "Eve", Age = 25 },
    new { Name = "Bob", Age = 20 }
};

var sorted = people
    .OrderBy(p => p.Age)     // najpierw po wieku
    .ThenBy(p => p.Name);    // potem po imieniu
```

## Najlepsze Praktyki

1. **U¿ywaj odroczonego wykonania m¹drze**
   ```csharp
   // le - zapytanie wykonuje siê dwa razy
   var query = numbers.Where(x => x > 5);
   var count = query.Count();
   var first = query.FirstOrDefault();

   // Dobrze - materializuj wyniki jeœli u¿ywasz ich wielokrotnie
   var results = numbers.Where(x => x > 5).ToList();
   var count = results.Count;
   var first = results.FirstOrDefault();
   ```

2. **Unikaj niepotrzebnej materializacji**
   ```csharp
   // le - niepotrzebne ToList()
   var result = numbers
       .Where(x => x > 5)
       .ToList()
       .Select(x => x * 2);

   // Dobrze - materializacja tylko gdy potrzebna
   var result = numbers
       .Where(x => x > 5)
       .Select(x => x * 2);
   ```

3. **Wykorzystuj typ IEnumerable<T> dla odroczonych operacji**
   ```csharp
   // Dobrze - zachowuje elastycznoœæ wykonania
   IEnumerable<int> GetFilteredNumbers(IEnumerable<int> numbers)
   {
       return numbers.Where(x => x > 0);
   }
   ```

## Podsumowanie

LINQ to potê¿ne narzêdzie, które znacz¹co upraszcza pracê z kolekcjami w C#. Kluczem do efektywnego wykorzystania LINQ jest zrozumienie ró¿nicy miêdzy natychmiastowym a odroczonym wykonaniem oraz œwiadome wykorzystywanie tych mechanizmów w kodzie.

Pamiêtaj:
- U¿ywaj odroczonych operacji gdy to mo¿liwe
- Materializuj wyniki tylko gdy jest to konieczne
- Zwracaj uwagê na wydajnoœæ przy z³o¿onych zapytaniach
- Wykorzystuj odpowiednie operatory do konkretnych zadañ