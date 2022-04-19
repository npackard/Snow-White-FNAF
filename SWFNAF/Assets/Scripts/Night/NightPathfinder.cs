using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightPathfinder : MonoBehaviour
{
    public NightPathfinder S;

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
        all = new List<Location>{Location.dwarfBedroom, Location.bathroom, Location.workshop, Location.unknown, Location.hallOne, Location.hallTwo, Location.livingRoom, Location.kitchen};
        GetKeys();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<List<Location>> Setup(Dwarf dwarf) {
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

    public void SleepyPath() {
        List<List<Location>> edges = Setup(Dwarf.sleepy);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
        // if the bedroom is unlocked, start in the bedroom
        // otherweise, start in the living room
        Location start;
        if (key4) start = Location.dwarfBedroom;
        else start = Location.livingRoom;
    }

    public void BasfhulPath() {
        List<List<Location>> edges = Setup(Dwarf.bashful);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
        Location start = Location.kitchen;
    }

    public void DocPath() {
        List<List<Location>> edges = Setup(Dwarf.doc);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
    }

    public void SneezyPath() {
        List<List<Location>> edges = Setup(Dwarf.sneezy);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
    }

    public void HappyPath() {
        List<List<Location>> edges = Setup(Dwarf.happy);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
    }

    public void GrumpyPath() {
        List<List<Location>> edges = Setup(Dwarf.grumpy);
        List<int> distances = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};
    }

    private void GetKeys() {
        key1 = PlayerPrefs.GetInt("Key1") == 1;
        key2 = PlayerPrefs.GetInt("Key2") == 1;
        key3 = PlayerPrefs.GetInt("Key3") == 1;
        key4 = PlayerPrefs.GetInt("Key4") == 1;
    }
}
