using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightPathfinder : MonoBehaviour
{
    public static NightPathfinder S;

    public List<Location> sleepyDwarfBedroomNeighbors;
    public List<Location> sleepyBathroomNeighbors;
    public List<Location> sleepyWorkshopNeighbors;
    public List<Location> sleepyUnknownNeighbors;
    public List<Location> sleepyHallOneNeighbors;
    public List<Location> sleepyHallTwoNeighbors;
    public List<Location> sleepyLivingRoomNeighbors;
    public List<Location> sleepyKitchenNeighbors;

    public List<Location> bashfulDwarfBedroomNeighbors;
    public List<Location> bashfulBathroomNeighbors;
    public List<Location> bashfulWorkshopNeighbors;
    public List<Location> bashfulUnknownNeighbors;
    public List<Location> bashfulHallOneNeighbors;
    public List<Location> bashfulHallTwoNeighbors;
    public List<Location> bashfulLivingRoomNeighbors;
    public List<Location> bashfulKitchenNeighbors;

    public List<Location> docDwarfBedroomNeighbors;
    public List<Location> docBathroomNeighbors;
    public List<Location> docWorkshopNeighbors;
    public List<Location> docUnknownNeighbors;
    public List<Location> docHallOneNeighbors;
    public List<Location> docHallTwoNeighbors;
    public List<Location> docLivingRoomNeighbors;
    public List<Location> docKitchenNeighbors;

    public List<Location> sneezyDwarfBedroomNeighbors;
    public List<Location> sneezyBathroomNeighbors;
    public List<Location> sneezyWorkshopNeighbors;
    public List<Location> sneezyUnknownNeighbors;
    public List<Location> sneezyHallOneNeighbors;
    public List<Location> sneezyHallTwoNeighbors;
    public List<Location> sneezyLivingRoomNeighbors;
    public List<Location> sneezyKitchenNeighbors;

    public List<Location> happyDwarfBedroomneighbors;
    public List<Location> happyBathroomNeighbors;
    public List<Location> happyWorkshopNeighbors;
    public List<Location> happyUnknownNeighbors;
    public List<Location> happyHallOneNeighbors;
    public List<Location> happyHallTwoNeighbors;
    public List<Location> happylivingRoomNeighbors;
    public List<Location> happyKitchenNeighbors;
    
    public List<Location> grumpyDwarfBedroomNeighbors;
    public List<Location> grumpyBathroomNeighbors;
    public List<Location> grumpyWorkshopNeighbors;
    public List<Location> grumpyUnknownNeighbors;
    public List<Location> grumpyHallOneNeighbors;
    public List<Location> grumpyHallTwoNeighbors;
    public List<Location> grumpyLivingRoomNeighbors;
    public List<Location> grumpyKitchenNeighbors;

    private List<Location> all;

    bool key1; // study
    bool key2; // workspace
    bool key3; // bathroom
    bool key4; // dwarf bedroom

    private void Awake() {
        if (NightGameManager.S) {
            Destroy(this.gameObject);
        } else {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetKeys();
        //Setup(Dwarf.sleepy);
        //SleepyPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<List<Location>> Setup(Dwarf dwarf) {
        List<Location> dwarfBedroom;
        List<Location> bathroom;
        List<Location> workshop;
        List<Location> unknown;
        List<Location> hallOne;
        List<Location> hallTwo;
        List<Location> livingRoom;
        List<Location> kitchen;
        switch(dwarf) {
            case Dwarf.sleepy:
                dwarfBedroom = new List<Location>(sleepyDwarfBedroomNeighbors);
                bathroom = new List<Location>(sleepyBathroomNeighbors);
                workshop = new List<Location>(sleepyWorkshopNeighbors);
                unknown = new List<Location>(sleepyUnknownNeighbors);
                hallOne = new List<Location>(sleepyHallOneNeighbors);
                hallTwo = new List<Location>(sleepyHallTwoNeighbors);
                livingRoom = new List<Location>(sleepyLivingRoomNeighbors);
                kitchen = new List<Location>(sleepyKitchenNeighbors);
                break;
            case Dwarf.bashful:
                dwarfBedroom = new List<Location>(bashfulDwarfBedroomNeighbors);
                bathroom = new List<Location>(bashfulBathroomNeighbors);
                workshop = new List<Location>(bashfulWorkshopNeighbors);
                unknown = new List<Location>(bashfulUnknownNeighbors);
                hallOne = new List<Location>(bashfulHallOneNeighbors);
                hallTwo = new List<Location>(bashfulHallTwoNeighbors);
                livingRoom = new List<Location>(bashfulLivingRoomNeighbors);
                kitchen = new List<Location>(bashfulKitchenNeighbors);
                break;
            case Dwarf.doc:
                dwarfBedroom = new List<Location>(docDwarfBedroomNeighbors);
                bathroom = new List<Location>(docBathroomNeighbors);
                workshop = new List<Location>(docWorkshopNeighbors);
                unknown = new List<Location>(docUnknownNeighbors);
                hallOne = new List<Location>(docHallOneNeighbors);
                hallTwo = new List<Location>(docHallTwoNeighbors);
                livingRoom = new List<Location>(docLivingRoomNeighbors);
                kitchen = new List<Location>(docKitchenNeighbors);
                break;
            case Dwarf.sneezy:
                dwarfBedroom = new List<Location>(sneezyDwarfBedroomNeighbors);
                bathroom = new List<Location>(sneezyBathroomNeighbors);
                workshop = new List<Location>(sneezyWorkshopNeighbors);
                unknown = new List<Location>(sneezyUnknownNeighbors);
                hallOne = new List<Location>(sneezyHallOneNeighbors);
                hallTwo = new List<Location>(sneezyHallTwoNeighbors);
                livingRoom = new List<Location>(sneezyLivingRoomNeighbors);
                kitchen = new List<Location>(sneezyKitchenNeighbors);
                break;
            case Dwarf.happy:
                dwarfBedroom = new List<Location>(happyDwarfBedroomneighbors);
                bathroom = new List<Location>(happyBathroomNeighbors);
                workshop = new List<Location>(happyWorkshopNeighbors);
                unknown = new List<Location>(happyUnknownNeighbors);
                hallOne = new List<Location>(happyHallOneNeighbors);
                hallTwo = new List<Location>(happyHallTwoNeighbors);
                livingRoom = new List<Location>(happylivingRoomNeighbors);
                kitchen = new List<Location>(happyKitchenNeighbors);
                break;
            case Dwarf.grumpy:
                dwarfBedroom = new List<Location>(grumpyDwarfBedroomNeighbors);
                bathroom = new List<Location>(grumpyBathroomNeighbors);
                workshop = new List<Location>(grumpyWorkshopNeighbors);
                unknown = new List<Location>(grumpyUnknownNeighbors);
                hallOne = new List<Location>(grumpyHallOneNeighbors);
                hallTwo = new List<Location>(grumpyHallTwoNeighbors);
                livingRoom = new List<Location>(grumpyLivingRoomNeighbors);
                kitchen = new List<Location>(grumpyKitchenNeighbors);
                break;
            default:
                dwarfBedroom = new List<Location>();
                bathroom = new List<Location>();
                workshop = new List<Location>();
                unknown = new List<Location>();
                hallOne = new List<Location>();
                hallTwo = new List<Location>();
                livingRoom = new List<Location>();
                kitchen = new List<Location>();
                break;
        }

        List<List<Location>> lists = new List<List<Location>>();
        lists.Add(dwarfBedroom);
        lists.Add(bathroom);
        lists.Add(workshop);
        lists.Add(unknown);
        lists.Add(hallOne);
        lists.Add(hallTwo);
        lists.Add(livingRoom);
        lists.Add(kitchen);

        GetKeys();

        foreach(List<Location> room in lists) {
            if (key1 && room.Contains(Location.unknown)) {
                room.Remove(Location.unknown);
            }
            if (key2 && room.Contains(Location.workshop)) {
                room.Remove(Location.workshop);
            }
            if (key3 && room.Contains(Location.bathroom)) {
                room.Remove(Location.bathroom);
            }
            if (key4 && room.Contains(Location.dwarfBedroom)) {
                room.Remove(Location.dwarfBedroom);
            }
        }

        return lists;
    }

    public List<Location> SleepyPath() {
        List<List<Location>> edges = Setup(Dwarf.sleepy);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
        // if the bedroom is unlocked, start in the bedroom
        // otherweise, start in the living room
        Location start;
        if (key4) start = Location.dwarfBedroom;
        else start = Location.livingRoom;
        return Dijkstra(edges, distances, start);
    }

    public List<Location> BasfhulPath() {
        List<List<Location>> edges = Setup(Dwarf.bashful);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
        Location start = Location.kitchen;
        if (key3) start = Location.bathroom;
        else start = Location.kitchen;
        return Dijkstra(edges, distances, start);
    }

    public List<Location> DocPath() {
        List<List<Location>> edges = Setup(Dwarf.doc);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
        Location start = Location.bathroom;
        return Dijkstra(edges, distances, start);
    }

    public List<Location> SneezyPath() {
        List<List<Location>> edges = Setup(Dwarf.sneezy);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
        Location start = Location.dwarfBedroom;
        return Dijkstra(edges, distances, start);
    }

    public List<Location> HappyPath() {
        List<List<Location>> edges = Setup(Dwarf.happy);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
        Location start = Location.unknown;
        return Dijkstra(edges, distances, start);
    }

    public List<Location> GrumpyPath() {
        List<List<Location>> edges = Setup(Dwarf.grumpy);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
        Location start = Location.workshop;
        return Dijkstra(edges, distances, start);
    }

    private void GetKeys() {
        key1 = PlayerPrefs.GetInt("Key1") == 1;
        key2 = PlayerPrefs.GetInt("Key2") == 1;
        key3 = PlayerPrefs.GetInt("Key3") == 1;
        key4 = PlayerPrefs.GetInt("Key4") == 1;
    }

    private List<Location> Dijkstra(List<List<Location>> edges, List<int> distances, Location first) {
        all = new List<Location>{Location.dwarfBedroom, Location.bathroom, Location.workshop, Location.unknown, Location.hallOne, Location.hallTwo, Location.livingRoom, Location.kitchen};
        int start;
        switch(first) {
            case (Location.dwarfBedroom):
                start = 0;
                break;
            case (Location.bathroom):
                start = 1;
                break;
            case (Location.workshop):
                start = 2;
                break;
            case (Location.unknown):
                start = 3;
                break;
            case (Location.hallOne):
                start = 4;
                break;
            case (Location.hallTwo):
                start = 5;
                break;
            case (Location.livingRoom):
                start = 6;
                break;
            case (Location.kitchen):
                start = 7;
                break;
            default:
                start = -1;
                break;
        }
        List<int> visited = new List<int>(distances);
        List<int> infs = new List<int>{999, 999, 999, 999, 999, 999, 999, 999};
        List<Location> locBefore = new List<Location>{Location.none, Location.none, Location.none, Location.none, Location.none, Location.none, Location.none, Location.none};
        List<List<int>> cost = new List<List<int>> {new List<int>(infs), new List<int>(infs),new List<int>(infs),new List<int>(infs),new List<int>(infs),new List<int>(infs),new List<int>(infs),new List<int>(infs)};
        for (int i = 0; i < all.Count; i++) {
            for (int j = 0; j < all.Count; j++) {
                cost[i][j] = 999;
                foreach(Location lol in edges[i]) {
                    if (lol == all[j]) {
                        cost[i][j] = 1;
                    }
                }
            }
        }

        for(int i = 0; i < all.Count; i++) {
            distances[i] = cost[start][i];
            visited[i] = 0;
        }

        distances[start] = 0;
        visited[start] = 1;
        int count = 1;
        int nextNode = 0;

        while (count < all.Count) {
            int minDistance = 999;

            for (int i = 0; i < all.Count; i++) {
                if (distances[i] < minDistance && visited[i] == 0) {
                    minDistance = distances[i];
                    nextNode = i;
                }
            }

            visited[nextNode] = 1;
            for (int i = 0; i < all.Count; i++) {
                if (visited[i] == 0 && minDistance + cost[nextNode][i] < distances[i]) {
                    distances[i] = minDistance + cost[nextNode][i];
                    locBefore[i] = all[nextNode];
                }
            }

            count++;
        }

        for (int i = 0; i < all.Count; i++) {
            if (distances[i] < 999 && locBefore[i] == Location.none) locBefore[i] = first;
        }

        return locBefore;
    }
}
