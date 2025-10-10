#include "binpackingFuncs.h"
#include <iostream>
using namespace std;

int main() 
{
    setlocale(LC_ALL, "Russian");
    cout << "Демонстрация алгоритма упаковки (First Fit)\n";
    cout << "Пример: если ввод =\n6 10\n2 5 4 7 1 3\n";
    cout << "ожидается 3 контейнера.\n\n";

    vector<int> items;
    int capacity;

    readInput(items, capacity);
    printItems(items);

    auto bins = firstFitPack(items, capacity);
    printResult(bins);

    cout << "\nСпасибо за использование программы!\n";
    return 0;
}