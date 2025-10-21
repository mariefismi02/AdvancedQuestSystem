# AdvancedQuestSystem

Easy to use quest system for Unity — a modular quest framework using ScriptableObjects.

## Installation (via Unity Package Manager / Git)
1. In Unity: Window > Package Manager.
2. Click "+" → "Add package from git URL...".
3. Enter the repository URL:
   - https://github.com/mariefismi02/AdvancedQuestSystem.git
4. To install a specific tag/branch/commit, append `#tag` (e.g. `#v1.0.0`).
5. Click "Add". Unity will fetch and install the package.

Note: replace mariefismi02 with the repo owner. For private repos, provide credentials or use a deploy key.

## Samples
To import example content, do NOT manually copy folders. In Unity:
1. Open Window > Package Manager.
2. Select "AdvancedQuestSystem" in the list.
3. Open the "Samples" tab and click the Import button for the sample you want.
This imports sample assets properly into your project.

## Quick Start
The recommended workflow to create quests and supporting scripts:

1. Ownership type
   - Create an ownership class that represents "who owns" the quest (e.g., Player).
   - That class is used to receive rewards. Mark it with the attribute:
     `[QuestOwnership]`

    ````csharp
    using mariefismi02.Quest;
    using UnityEngine;

    [QuestOwnership]
    public class Player : MonoBehaviour
    {
        public void AddExp(int exp)
        {
            Debug.Log("Player received exp: " + exp);
        }

        public void AddItem(string itemId, int amount)
        {
            Debug.Log("Player received item: " + itemId + " x" + amount);
        }
    }
    ````

2. Objectives
   - Create objective scripts for your game needs by implementing the `QuestObjective` interface/class (example: `CollectItemObjective`).
   - Mark each objective type with: `[QuestComposite]`

   ````csharp
    using mariefismi02.Quest;
    using UnityEngine;

    [QuestComposite]
    public class CollectItemObjective : QuestObjective
    {
        private string itemId;
        private int currentCount, targetCount;

        //TODO: pindahin ke scriptable object
        public override string Description => $"Collect {targetCount} of item '{itemId}'";

        public CollectItemObjective(string itemId, int targetCount)
        {
            this.itemId = itemId;
            this.targetCount = targetCount;
        }

        private void OnCollectItem(string id)
        {
            if (id != itemId)
            {
                return;
            }

            if (IsCompleted)
            {
                return;
            }

            currentCount++;

            OnProgressUpdated.Invoke(currentCount, targetCount);

            if (currentCount >= targetCount)
            {
                IsCompleted = true;
                OnCompleted.Invoke();
            }
        }
        
        public override void Init()
        {
            GameEvent.Instance.OnCollectItem.AddListener(OnCollectItem);
        }

        public override void Cleanup()
        {
            GameEvent.Instance.OnCollectItem.RemoveListener(OnCollectItem);
        }

        public override int GetProgressValue()
        {
            var progress = currentCount / (float)targetCount;
            return Mathf.RoundToInt(progress * 100);
        }

        public override void SetProgressValue(int value)
        {
            currentCount = (targetCount * value) / 100;
        }

        public override (int current, int target) GetProgress()
        {
            return (currentCount, targetCount);
        }
    }
    ````

3. Rewards
   - Create reward scripts by deriving from `QuestReward<T>` where T is your ownership class (e.g., `QuestReward<Player>`).
   - Example: `ExpReward : QuestReward<Player>`
   - Mark rewards with: `[QuestReward(typeof(Player))]` — ownership type is required for rewards.

   ````csharp
    using mariefismi02.Quest;
    using UnityEngine;

    [QuestComposite(typeof(Player))]
    public class ExpReward : IQuestReward<Player>
    {
        private int expAmount;

        public ExpReward(int expAmount)
        {
            this.expAmount = expAmount;
        }

        public void Give(Player target)
        {
            target.AddExp(expAmount);
        }
    }
    ````

4. Why attributes
   - `[QuestOwnership]` and `[QuestComposite]` are required by the editor generator so it can discover types and auto-generate the Quest ScriptableObject asset script.

5. Generating asset scripts (Editor)
   - In Unity menu: Tools > Quest > Asset Script Generator.
   - Select the Ownership Type from the dropdown.
   - Click "Generate Quest Asset Script for Selected Ownership Type" to generate a quest asset ScriptableObject tailored for that ownership.
   - Click "Generate All Composite Data Scripts" to generate editor composite scripts for discovered objectives and rewards.

6. Creating quest assets
   - After generation, right-click in the Project window and create a quest asset via Create > Quests.

7. Finish
   - Implement runtime logic (IQuestSaveable, quest acceptance, progression, completion and reward application) using the generated asset and your Ownership/Objective/Reward types.

This workflow ensures editor-driven asset generation and correct wiring between ownership, objectives, and rewards.

## Important files
- `Runtime/Quest.cs` — quest data model.
- `Runtime/IQuestSaveable.cs` — save/load interface.
- `Editor/QuestAssetScriptGenerator.cs` — editor helpers.
- `Samples~/PlayerQuest` — example content (import via Package Manager Samples).

## Future plan
- Auto Complete option in quest asset.
- Unlock requirement (optional) in quest asset.
- Separated assembly (or package if needed) for a modular and more customizable save system — allows implementing custom save backends (for example using a 3rd-party cloud service instead of local save).