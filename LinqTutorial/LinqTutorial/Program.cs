using Dumpify;

IEnumerable<int> collection = [1, 2, 3, 4, 5, 6];

// Where - Filtrowanie

//collection.Where(x=> x>=2).Dump();

//Oftype - Filtruj tylko typy

//collection.OfType<int>().Dump();
//collection.OfType<string>().Dump();

// Skip - pomijanie elementów
//collection.Skip(3).Dump();
// SkipLast - pomijanie ostatnich elementów
// SkipWhile - Skipuje elementy dopóki dany warunek jest spełniony


// Take - branie danej liczby pierwszych elementow 
//collection.Take(3).Dump();
//TakeLast - branie danej liczby ostatnich elementów
//TakeWhile - pobiera elementy dopóki dany warunek zwraca true

// Select - pobiera elementy i zamienia ja na dany typ
collection.Select(x=> x.ToString()).Dump();
// Można overloadować go poprzed podanie parametru drugiego który jest indeksem

//SelectMany  - stosowant do spłaszczania(flatten) tablic jako jedna kolekcja

//Cast  zmienia typ kolekcji

//Chunk - splituje kolekcje tak aby zawierały tyle elementów ile podane


//Any - sprwadza czy istnieje elemnty ktory spelnia warunek
//All - sprawdza czy wszystkie elementy spelniają warunek

//contains - sprawdza czy kolekcja zawiera dany element


//Append - dodaje do kolekcji coś na końcu
//Prepend - dodaje do kolekcji coś na początku

//Count - Zwraca liczbę elementów

//Maxby - zwraca obiekt która spełnia warunek bycia maksymalną wartość

//Sum - coś jak reduce dodaje elementy do siebie
//Average - średnia

//Aggregate - reduce JS

collection.Aggregate((x,y) => x+y).Dump();

//Overload 2
collection
    .Aggregate(10, (x, y) => x + y)
    .Dump();

//Overload 3
collection
    .Aggregate(10, (x, y) => x +y,x=> x/2)
    .Dump();


//ToArray - do tablicy
//ToList - do listy
//ToDictionary - do słownika (key,value)
//ToHashSet - do hash setu
//ToLookup - Do grupy na podstawie jakiegoś parametru

//Zip tworzy tuple na podstawie dwoch kolekcji, paruje je razem

//Sorting

//Order - posortuje kolekcje
//Możemy sortować np po wieku w obiekcei x=> x.age
//OrderDescending - sortuje malejąco

//ThenBy - po czym sortowac następnie

//Reverse - sortuje na odwrot

