# Anno 1800 Planner Documentation
# About the Project
This document outlines the core concepts of the Anno 1800 Planner, a desktop application designed to help players calculate and plan their in-game production chains and population needs.

# Core Architecture
The application is built using the Model-View-ViewModel (MVVM) pattern. This documentation focuses on the Model component, specifically the static data layer that holds all core game information.

# The GameData Layer
The GameData layer serves as a static, in-memory database containing all fundamental game entities like buildings, resources, and population needs. This data is considered static and is not editable by the user during runtime.

The long-term goal is to load this data from an external JSON file, but it is currently initialized within the code.

The Game Class: Central Access Point
All static game data is loaded at startup and accessed via the static Game class. This provides a single, consistent entry point for the rest of the application (e.g., the ViewModels) to retrieve game information.

Core Concept: Any part of the application can get static definitions without needing to manage its own data store.

C#

// Example: Retrieving static data from anywhere in the application
Building sawmill = Game.Get(BuildingId.Sawmill);
Resource timber = Game.Get(ResourceId.Timber);
ResidentTier artisanTier = Game.Get(TierId.Artisans);
Key Data Objects
The GameData layer is composed of several key classes that model in-game concepts.

Building
Represents a single production or service building.

Concept: A Building definition holds its static properties like Maintenance cost and ProductionTime.
Production: A building knows what resources it requires and produces (e.g., a Sawmill Requires Wood and Produces Timber). The responsibility for calculating the optimal ratio of buildings belongs to a higher-level calculator logic, which uses the ProductionTime of the related buildings.
Resource
Represents a good, raw material, or intermediate product (e.g., Wood, Wheat, Steel). This is a simple data object used primarily for identification.

ResidentTier
Represents a population level (e.g., Farmers, Workers, Artisans).

Concept: Each tier acts as a container for the Need objects associated with it. This allows the application to easily find all requirements for a given population type.
Need
Represents a single requirement for a population tier to be satisfied.

Concept: A Need can be one of two types:

A Resource Need (e.g., access to Fish).
A Building Need (e.g., access to a Marketplace).
Consumption: For resource needs, the Consumption property stores the reciprocal of the standard consumption rate. It represents how many houses can be supplied for one minute by a single ton of the resource. This allows for cleaner data values (e.g., integers).

Example: A value of 400 for Fish means that one ton of Fish can supply 400 houses for one minute.