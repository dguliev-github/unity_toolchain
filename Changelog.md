# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.3] - 2024-01-30

### Added
- multi LOD group cross-fade editing

## [1.0.2] - 2023-12-13

### Added
- Collider manipulation scripts.
- LOD group manipulation scripts.

### Removed
- `Generate prefabs` script. The standard method of generating prefabs by dragging multiple gameobjects into the Project window seems to be more efficient. 

## [1.0.1] - 2023-12-05

### Added
- This changelog file.

### Fixed

- .meta file error spam.
- Copies of every editor window in `Menu/Window/Panels/` due to forced instancing on each OnEnable().

### Changed

- Refactored whole project to use OnGUI() static methods.
- Minor changes in documentation.

## [1.0.0] - 2023-08-15

### Added

- Consolidated all scripts into one window 