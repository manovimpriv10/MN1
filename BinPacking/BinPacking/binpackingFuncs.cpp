#include "binpackingFuncs.h"
#include <iostream>

void readInput(vector<int>& items, int& capacity) {
    cout << "¬ведите количество предметов и вместимость контейнера:\n";
    int n;
    cin >> n >> capacity;
    items.resize(n);
    cout << "¬ведите веса предметов:\n";
    for (int i = 0; i < n; ++i)
        cin >> items[i];
}

void printItems(const vector<int>& items) {
    cout << "¬ведены предметы: ";
    for (int w : items) 
        cout << w << " ";
    cout << "\n";
}

void simplePack(const vector<int>&, int) {
    cout << "[simplePack] пока не реализована\n";
}