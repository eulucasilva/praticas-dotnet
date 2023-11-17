#region Exercicio 3
    double numeroDouble = 10.75;
    int numeroInteiro = (int)numeroDouble;

        
    Console.WriteLine($"Número Double: {numeroDouble}");
    Console.WriteLine($"Número Inteiro: {numeroInteiro}");

    //Ao converter para int, a parte fracionária será truncada, resultando em numeroInteiro sendo igual a 10.
#endregion

#region Exercicio 4
    int x = 10;
    int y = 3;

    Console.WriteLine($"Adição {x+y}");
    Console.WriteLine($"Subtração {x-y}");
    Console.WriteLine($"Multiplicação {x*y}");
    Console.WriteLine($"Divisão {x/y}");
#endregion

#region Exercicio 5
    int a = 5;
    int b = 8;

    if (a > b){
        Console.WriteLine($"A é maior que B");
    }
    else{
        Console.WriteLine($"A não é maior que B");
    }
#endregion

#region Exercicio 6
    string str1 = "Hello";
    string str2 = "World";

    if (str1 == str2){
        Console.WriteLine($"São string iguais");
    }
    else{
        Console.WriteLine($"Não são strings iguais");
    }
#endregion

#region Exercicio 7
    bool condicao1 = true;
    bool condicao2 = false;

    if (condicao1){
        Console.WriteLine($"Esta condição é verdadeira");
    }
    else{
        Console.WriteLine($"Esta condição é falsa");
    }

    if (condicao2){
        Console.WriteLine($"Esta condição é verdadeira");
    }
    else{
        Console.WriteLine($"Esta condição é falsa");
    }
#endregion

#region Exercicio 8
    int num1 = 7;
    int num2 = 3;
    int num3 = 10;

    if (num1 > num2){
        Console.WriteLine($"Num1 é maior que Num2");
    }
    else{
        Console.WriteLine($"Num1 não é maior que Num2");
    }

    if (num3 == num1 + num2){
        Console.WriteLine($"Num3 é a soma de Num1 e Num2");
    }
    else{
        Console.WriteLine($"Num3 não é a soma de Num1 e Num2");
    }

#endregion