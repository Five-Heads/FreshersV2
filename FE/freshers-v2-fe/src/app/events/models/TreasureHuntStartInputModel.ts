export class TreasureHuntStartInputModel {
  constructor(
    public Id: number,

    public TotalCheckpoints: number,

    public GroupId: number,

    public GroupMembers: number[],

    public next: CheckpointInputModel,

    public nextReachedBy: string[] = []
  ) {
  }
}
export class CheckpointInputModel {

    public id: number = 0
    public question: string = ''
    public isFinal: boolean = false
    public assignPerson: string = ''
    public orderNumber: number = 0
    public nextReachedBy: string[] = []

}
