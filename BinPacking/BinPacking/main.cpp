#include "binpackingFuncs.h"
#include <iostream>

int main() {
    setlocale(LC_ALL, "Russian");
    vector<int> items;
    int capacity = 0;

    readInput(items, capacity);
    printItems(items);
    simplePack(items, capacity);

    return 0;
}