import {CheckpointCreateOutput} from "./CheckpointCreateOutput";

export class EventCreateOutput {
  Name: string = '';
  StartTime: string = '';
  EndTime: string = '';

  Checkpoints: CheckpointCreateOutput[] = [];
}
