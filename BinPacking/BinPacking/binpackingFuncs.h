#pragma once

#include <vector>
#include <iostream>

struct Container {
    int remaining;
    std::vector<int> items;
};

void readInput(std::vector<int>& items, int& capacity);
void printItems(const std::vector<int>& items);
std::vector<Container> firstFitPack(const std::vector<int>& items, int capacity);
void printResult(const std::vector<Container>& bins);