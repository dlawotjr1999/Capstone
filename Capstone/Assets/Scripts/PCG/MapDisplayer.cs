using System;
using System.Collections.Generic;
using System.Linq;
using PCG.Data_Structures;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public class MapDisplayer
{
    private const int CellSize = 4;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public GameObject CellsParent { get; private set; }
    public GameObject GroundPrototypePrefab { get; private set; }
    public GameObject MidPointPrototypePrefab { get; private set; }
    public GameObject RoomInfoPanelParent { get; private set; }
    public GameObject RoomInfoPanelPrefab { get; private set; }
    public GameObject WallPrototypePrefab { get; private set; }
    public GameObject CorridorPrototypePrefab { get; private set; }
    public TextMeshProUGUI GenerationText { get; private set; }
    
    public MapDisplayer(int width,
        int height,
        GameObject cellsParent,
        GameObject groundPrototypePrefab,
        GameObject midPointPrototypePrefab,
        GameObject roomInfoPanelParent,
        GameObject roomInfoPanelPrefab,
        GameObject wallPrototypePrefab,
        GameObject corridorPrototypePrefab,
        TextMeshProUGUI generationText)
    {
        Width = width;
        Height = height;
        CellsParent = cellsParent;
        GroundPrototypePrefab = groundPrototypePrefab;
        MidPointPrototypePrefab = midPointPrototypePrefab;
        RoomInfoPanelParent = roomInfoPanelParent;
        RoomInfoPanelPrefab = roomInfoPanelPrefab;
        WallPrototypePrefab = wallPrototypePrefab;
        CorridorPrototypePrefab = corridorPrototypePrefab;
        GenerationText = generationText;
    }

    public void RemoveRoomInfoAndCells()
    {
        foreach (Transform child in RoomInfoPanelParent.transform)
            GameObject.Destroy(child.gameObject);
        
        foreach (Transform child in CellsParent.transform)
            GameObject.Destroy(child.gameObject);
    }
    
    public void LogRoomInfo(List<Room> rooms)
    {
        GenerationText.text += $"\nTotal rooms: {rooms.Count}";
        foreach (var room in rooms)
        {
            var roomInfoPanel = GameObject.Instantiate(RoomInfoPanelPrefab, RoomInfoPanelParent.transform);
            roomInfoPanel.name = $"Room {room.RoomNumber}";
            
            var roomInfoText = roomInfoPanel.GetComponentInChildren<TextMeshProUGUI>();
            roomInfoText.text = $"Room {room.RoomNumber}:" +
                                $"\tCenter: ({room.CenterCell.X}, {room.CenterCell.Y})\n" +
                                
                                $"\tSize: {room.RoomCells.Count}" +
                                $"\tTop Left: ({room.X}, {room.Y})\n" +
                                
                                $"\tWidth: {room.Width}" +
                                $"\tHeight: {room.Height}";
            
            // log each cell in room
            // Debug.Log($"Room {room.RoomNumber}\nRoom Cells: {string.Join(", ", room.RoomCellsRelative.Select(cell => $"({cell.X}, {cell.Y})"))}\nWall Cells: {string.Join(", ", room.WallCellsRelative.Select(cell => $"({cell.X}, {cell.Y})"))}");
        }
    }

    public void LogMap(Map map, int currentGeneration)
    {
        // var cellSize = GroundPrototypePrefab.GetComponent<RectTransform>().rect.width * 2f;
        // var screenTopLeft = new Vector3(-Width * CellSize / 2, Height * CellSize / 2, 0);
        
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                var cell = map.Cells[y, x];
                CreateCell(cell);
            }
        
        GenerationText.text = $"Generation {currentGeneration}";
    }

    public void CreateCell(Cell cell)
    {
        var cellType = cell.Type;
        GameObject cellObject = null;
        
        switch (cellType)
        {
            case CellType.Room:
            {
                if (cell is not RoomCell) 
                    return;
                
                cellObject = GameObject.Instantiate(((RoomCell)cell).IsCenter
                        ? MidPointPrototypePrefab
                        : GroundPrototypePrefab,
                    CellsParent.transform);
                
                cellObject.name = $"Room ({cell.X}, {cell.Y})";
                break;
            }
            
            case CellType.Wall:
            {
                cellObject = Object.Instantiate(WallPrototypePrefab, CellsParent.transform);
                cellObject.name = $"Wall ({cell.X}, {cell.Y})";
                break;
            }
            
            case CellType.Corridor:
            {
                cellObject = Object.Instantiate(CorridorPrototypePrefab, CellsParent.transform);
                cellObject.name = $"Corridor ({cell.X}, {cell.Y})";
                break;
            }
                
            case CellType.Blank:
            default:
                break;
        }
        
        if (!cellObject) 
            return;
                
        var position = new Vector3(cell.X * CellSize, 0, -cell.Y * CellSize);
        cellObject.transform.localPosition = position;
        var vertexMonoBehaviour = cellObject.AddComponent<VertexMonoBehaviour>();
        vertexMonoBehaviour.vertex = new Vertex(cell.X, cell.Y);

    }
    
    public void DisplayCorridors(Map map)
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                var cell = map.Cells[y, x];
                if (cell.Type == CellType.Corridor)
                    CreateCell(cell);
            }
    }
    
    public void LogMapText(Map rawMap)
    {
        var lines = "";
        for (var y = 0; y < Height; y++)
        {
            var line = "";
            for (var x = 0; x < Width; x++)
            {
                string roomType;
                switch (rawMap.Cells[y, x].Type)
                {
                    case CellType.Blank:
                        roomType = "-";
                        break;
                    case CellType.Room:
                        roomType = "R";
                        break;
                    case CellType.Wall:
                        roomType = "W";
                        break;
                    case CellType.Corridor:
                        roomType = "C";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                line += roomType;
            }

            lines += line + "\n";
        }
        
        Debug.Log(lines);
    }
}
