# Vampire Survivors 🧛

Description: 2D unity game in the style of Vampire Survivors, with some twists: there's infinite enemies and you have to survive as long as you can,
with the intention of reaching as high a level as possible. The difficulty spikes later into the game, and we made sure there's build options you can choose,
and randomized enemies so no playthrough is the same.

## User stories:
### MUST HAVE:
- a menu that allows me to customize my character, power up and have access to a variety of settings
- multiple choices towards a build that will let me advance into the game, that is met through different weapons or power ups/passives.
- Weapons can be a whip, a gun, a laser, etc;
- fitting soundtracks and sound effects that match the gameplay
- appeasing and pleasing visuals that blend together/ don't seem out of place.
- to accumulate experience through defeating enemies, that leads to leveling up and choosing between power ups
- Experierience is gained by gathering crystals that drop from enemies
- some enemies will drop treasures that drop significant power ups in order to vary the progression so it is more randomized
- Treasures can randomly boost your power level even more

SHOULD HAVE:
- the fight will be against ever stronger enemies as the game progresses
- mini-bosses that prove to be a harder challenge than usual and drop more interesting stuff
- difficulty that increases linearly
- a history of runs, so you can have an idea of how well you're progressing

NICE TO HAVE:
- to have secret encounters/ easter eggs that will reward my engagement in exploring the world

### Backlog:
https://trello.com/b/0l98WC9l/vampire-survivors

### Class hierarchy/UML:
- Found in the UML.html file, generated by doxygen

### Source control:
- Using git, with gitignore on the scenes.
- Most often used commands:
```
//for creating a local branch
git checkout -b NEW_BRANCH_NAME
git branch
git branch -r

//for adding changes to a remote branch
git status
git add .
git commit -m "message"
git push
git log

//for merging a branch into main
git checkout main
git merge local_branch_name

//for reverting changes
git revert .
git reset --hard origin/main

//for pulling remote changes
git pull
```
### Design patterns:

- Dependency injection

![image](https://github.com/Eronate/Vampire-Survivors/assets/99949441/ed4c413c-ea51-44ab-b5db-41a781481346)

- Interface:

![image](https://github.com/Eronate/Vampire-Survivors/assets/99949441/ea0f4635-dc9f-4ef8-af08-3c38c19cb79c)

- Singleton:

![image](https://github.com/Eronate/Vampire-Survivors/assets/99949441/6543bde2-17e1-41bb-b68c-0f23e98c878a)

### Code comments:
(in the spots where it was neccessary)

<img src="https://github.com/Eronate/Vampire-Survivors/assets/99949441/2962bb85-1d11-4130-b691-68391a492961" width=500px; height=400px;>

### Tool usage:
- Used ChatGPT to generate non-linear functions that were used throughout the game for certain scalings
- For example, the function responsible for calculating the experience cap for each level was made with the help of the tool:
![image](https://github.com/Eronate/Vampire-Survivors/assets/99949441/84582e83-7d86-4298-8a40-073fc05d66fe)
- Besides that, we used functions generated by ChatGPT were used in areas such as weapon cooldown scaling, garlic size scaling, etc..
