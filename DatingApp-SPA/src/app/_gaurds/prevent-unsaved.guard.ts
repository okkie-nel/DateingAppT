import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemebrEditComponent } from '../allMembers/memebr-edit/memebr-edit.component';

@Injectable()
export class PreventUnsaved implements CanDeactivate<MemebrEditComponent> {
    canDeactivate(component: MemebrEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Are you sure all unsaved changes will be lost');
        }
        return true;
    }
}
