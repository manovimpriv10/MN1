#include "binpackingFuncs.h"
#include <iostream>

int main() {
    vector<int> items;
    int capacity = 0;

    readInput(items, capacity);
    printItems(items);
    simplePack(items, capacity);

    return 0;
}