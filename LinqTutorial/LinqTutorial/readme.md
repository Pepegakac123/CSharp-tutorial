# LINQ (Language Integrated Query) - Przewodnik

## Spis tre�ci
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

LINQ (Language Integrated Query) to zestaw funkcjonalno�ci w .NET, kt�ry wprowadza mo�liwo�� wykonywania zapyta� bezpo�rednio w kodzie C#. LINQ upraszcza prac� z kolekcjami danych, bazami danych i XML.

## Typy Wykonania w LINQ

W LINQ wyst�puj� dwa typy wykonania zapyta�:

### 1. Immediate Execution (Natychmiastowe Wykonanie)
Operacje wykonywane od razu po wywo�aniu, zwracaj�ce wynik natychmiast:
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
Operacje wykonywane dopiero w momencie faktycznego u�ycia wynik�w:
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
Filtruje elementy okre�lonego typu:
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
Sp�aszcza zagnie�d�one kolekcje:
```csharp
var arrays = new[] { 
    new[] { 1, 2 }, 
    new[] { 3, 4 } 
};
var flattened = arrays.SelectMany(x => x);
// Wynik: [1, 2, 3, 4]
```

### Zip
��czy dwie kolekcje:
```csharp
var numbers = new[] { 1, 2, 3 };
var letters = new[] { "A", "B", "C" };
var zipped = numbers.Zip(letters, (n, l) => $"{l}{n}");
// Wynik: ["A1", "B2", "C3"]
```

## Operacje Agregacji

### Aggregate
Redukuje kolekcj� do pojedynczej warto�ci:
```csharp
var numbers = new[] { 1, 2, 3, 4 };

// Podstawowe u�ycie
var sum = numbers.Aggregate((x, y) => x + y);
// Wynik: 10

// Z warto�ci� pocz�tkow�
var sumPlusTen = numbers.Aggregate(10, (x, y) => x + y);
// Wynik: 20

// Z transformacj� wyniku
var average = numbers.Aggregate(
    0.0,                    // warto�� pocz�tkowa
    (acc, val) => acc + val,// akumulator
    result => result / 4    // transformacja ko�cowa
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

1. **U�ywaj odroczonego wykonania m�drze**
   ```csharp
   // �le - zapytanie wykonuje si� dwa razy
   var query = numbers.Where(x => x > 5);
   var count = query.Count();
   var first = query.FirstOrDefault();

   // Dobrze - materializuj wyniki je�li u�ywasz ich wielokrotnie
   var results = numbers.Where(x => x > 5).ToList();
   var count = results.Count;
   var first = results.FirstOrDefault();
   ```

2. **Unikaj niepotrzebnej materializacji**
   ```csharp
   // �le - niepotrzebne ToList()
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
   // Dobrze - zachowuje elastyczno�� wykonania
   IEnumerable<int> GetFilteredNumbers(IEnumerable<int> numbers)
   {
       return numbers.Where(x => x > 0);
   }
   ```

## Podsumowanie

LINQ to pot�ne narz�dzie, kt�re znacz�co upraszcza prac� z kolekcjami w C#. Kluczem do efektywnego wykorzystania LINQ jest zrozumienie r�nicy mi�dzy natychmiastowym a odroczonym wykonaniem oraz �wiadome wykorzystywanie tych mechanizm�w w kodzie.

Pami�taj:
- U�ywaj odroczonych operacji gdy to mo�liwe
- Materializuj wyniki tylko gdy jest to konieczne
- Zwracaj uwag� na wydajno�� przy z�o�onych zapytaniach
- Wykorzystuj odpowiednie operatory do konkretnych zada�