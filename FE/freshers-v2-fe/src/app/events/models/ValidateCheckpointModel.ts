public class ValidateCheckpointRequestModel
{
    public int GroupId { get; set; }

    public int CheckpointId { get; set; }

    public int TreasureHuntId { get; set; }
}

export class ValidateCheckpointModel{
    constructor(
        public GroupId: number,
        public CheckpointId: number,
        public TreasureHuntId: number
    ){}
}