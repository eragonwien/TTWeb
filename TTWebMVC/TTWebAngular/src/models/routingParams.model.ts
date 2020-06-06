export class RoutingParams {
  type: RoutingParamsType;
  id: number;

  public constructor(type: RoutingParamsType, id: number = 0) {
    this.type = type;
    this.id = id;
  }
}

export enum RoutingParamsType {
  create,
  edit,
}
