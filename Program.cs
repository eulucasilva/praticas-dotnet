#region Exercicio 1

/*dotnet --version exibirá a versão do .NET SDK instalada no seu sistema

dotnet --list-sdks lista todas as versões do .NET SDK instaladas no seu sistema

dotnet --uninstall-sdk <versão> remove uma versão específica do .NET SDK

dotnet --version O .NET CLI (Command Line Interface) tem a capacidade de atualizar automaticamente 
para a versão mais recente do SDK. Portanto, você pode usar o comando dotnet --version para verificar 
a versão atual e, se necessário, o CLI solicitará a atualização para a versão mais recente. */

#endregion

#region Exercicio 2

/*
sbyte:
Tamanho: 8 bits
Faixa: -128 a 127
Exemplo: sbyte valor = 10;

byte:
Tamanho: 8 bits
Faixa: 0 a 255
byte valor = 255;


short:
Tamanho: 16 bits
Faixa: -32,768 a 32,767
short valor = -15000;

ushort:
Tamanho: 16 bits
Faixa: 0 a 65,535
ushort valor = 50000;

int:
Tamanho: 32 bits
Faixa: -2,147,483,648 a 2,147,483,647
int valor = 1000000;

uint:
Tamanho: 32 bits
Faixa: 0 a 4,294,967,295
uint valor = 3000000000;


long:
Tamanho: 64 bits
Faixa: -9,223,372,036,854,775,808 a 9,223,372,036,854,775,807
long valor = 9000000000000000000;

ulong:
Tamanho: 64 bits
Faixa: 0 a 18,446,744,073,709,551,615
ulong valor = 15000000000000000000; */

#endregion

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