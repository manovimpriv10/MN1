#include "binpackingFuncs.h"
using namespace std;

int main() {
    setlocale(LC_ALL,"Russian");
    vector<int> items;
    int capacity;

    readInput(items, capacity);
    printItems(items);

    auto bins = firstFitPack(items, capacity);
    printResult(bins);

    return 0;
}
