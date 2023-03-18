export class TreasureHuntStartInputModel {
  constructor(
    public Id: number,

    public TotalCheckpoints: number,

    public GroupId: number,

    public GroupMembers: number[],

    public Next: CheckpointInputModel,
  ) {
  }
}
export class CheckpointInputModel {

    public Id: number = 0
    public Question: string = ''
    public IsFinal: boolean = false
    public AssignPerson: string = ''
    public OrderNumber: number = 0
    public NextReachedBy: number[] =[]

}
