# Dependency Graph
This repository contains the Dependency Graph implementation. This project tackles the problem of determining the order in which cells in a spreadsheet should be computed based on their dependencies.

## Problem Overview
In a spreadsheet, cells can contain formulas that reference other cells. For example:
* A1 contains the formula A2 + A3
* A2 contains the formula A3 * A4
* A3 and A4 contain formulas without variables
The challenge is to determine the order in which cells should be computed. For the example above:
* Compute A4 and A3
* Compute A2
* Compute A1
## Dependency Graph
This problem can be represented as a directed graph, where:
* Nodes represent spreadsheet cells (e.g., A1, A2).
* Edges represent dependencies between cells. An edge from node A to node B means A depends on B (i.e., B must be computed before A).

## Project Structure
* DependencyGraph/
  Contains the implementation of the dependency graph API.
* DependencyGraphApp/
  A console application to demonstrate the functionality of the dependency graph.
* DependencyGraphTest/
  Unit tests for individual functions and methods in the dependency graph.
* PS2GradingTests/
  A set of regression tests to validate the correctness of the dependency graph implementation.
* PS2.sln
  The solution file for the Visual Studio project.

## Features
Dependency Management
* Add and remove dependencies between nodes.
* Query the dependents and dependees of a given node.
Efficiency
* The graph implementation is optimized to handle queries and updates in a reasonable amount of time.
API
* The outward-facing API follows the provided specification and includes methods for managing dependencies.

## Testing
* Unit Tests
* Regression Testing
